using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace WordlistGenerator
{
    public class Engine
    {
        [JsonProperty("RulesConfig")]
        private readonly VirtualWordRulesConfig RulesConfig;

        [JsonProperty("VirtualWord")]
        private VirtualWord VirtualWord;

        private bool EndStatus;
        private bool RunStatus;
        private Thread ControllerThread;
        private readonly List<EngineThread> Threads;

        /// <summary>
        /// Get last computed word
        /// </summary>
        /// <returns></returns>
        public string GetLastWord() 
        {
            return this.VirtualWord.ToString();
        }

        public bool IsEnd() 
        {
            return this.EndStatus;
        }

        /// <summary>
        /// Clear generated words list
        /// </summary>
        public void ClearGeneratedWords() 
        {
            this.Threads.ForEach(t => t.ClearGeneratedWords());
        }

        /// <summary>
        /// Return how much words are generated
        /// </summary>
        /// <returns></returns>
        public int GetGeneratedWordsCount() 
        {
            return this.Threads.Sum(t => t.GetGeneratedWordsCount());
        }

        /// <summary>
        /// Return current genetared words list
        /// </summary>
        /// <returns></returns>
        public string[] GetGeneratedWords() 
        {
            var words = new List<string>();
            this.Threads.ForEach(t => words.AddRange(t.GetGeneratedWords()));

            return words.ToArray();
        }

        /// <summary>
        /// Start words computation thread
        /// </summary>
        public void StartProcess()
        {
            this.RunStatus = true;
            this.ControllerThread.IsBackground = true;
            this.ControllerThread.Start();

            this.Threads.ForEach(t => t.StartProcess());
        }

        /// <summary>
        /// End controller and other threads
        /// </summary>
        public void EndProcess() 
        {
            this.EndStatus = true;
            this.Threads.ForEach(t => t.EndProcess());
        }

        /// <summary>
        /// Pause controller and other threads execution
        /// </summary>
        public void Pause() 
        {
            this.RunStatus = false;
            this.Threads.ForEach(t => t.Pause());
        }

        /// <summary>
        /// Resume controller and other threads execution
        /// </summary>
        public void Resume() 
        {
            this.RunStatus = true;
            this.Threads.ForEach(t => t.Resume());
        }

        /// <summary>
        /// Save all current generated words
        /// </summary>
        private void SaveWords() 
        {
            var words = new List<string>();
            this.Threads.ForEach(t => words.AddRange(t.GetGeneratedWords()));

            File.AppendAllLines(RulesConfig.WordlistPath, words);    
        }

        /// <summary>
        /// Save current word list and create engine checkpoint
        /// to restore in future
        /// </summary>
        private void SaveCheckpoint() 
        {
            Pause();

            this.VirtualWord = Threads.First().GetVirtualWord();

            var checkpoint = new Checkpoint(this, VirtualWordRulesConfig.CheckpointPath);
            checkpoint.CreatePoint();

            SaveWords();
            ClearGeneratedWords();

            Resume();
        }

        /// <summary>
        /// Set function for computation words thread
        /// </summary>
        private void CreateControllerThread() 
        {
            this.ControllerThread = new Thread(() => 
            {
                while (this.EndStatus == false)
                {
                    Thread.Sleep(1000);

                    if(RunStatus)
                        if (GetGeneratedWordsCount() >= RulesConfig.CheckpointWordsCount)
                            SaveCheckpoint();
                }

                SaveCheckpoint();
            });
        }

        public void CreateThreads() 
        {
            var len = RulesConfig.ThreadsNumber;
            for (int i = 0; i < len; i++)
                this.Threads.Add(new EngineThread(i, len, RulesConfig, VirtualWord));
        }

        public Engine(VirtualWordRulesConfig rulesConfig) 
        {
            this.RulesConfig = rulesConfig;
            this.VirtualWord = new VirtualWord(rulesConfig);
            this.Threads = new List<EngineThread>();

            CreateControllerThread();
            CreateThreads();
        }

        [JsonConstructor]
        public Engine(VirtualWordRulesConfig RulesConfig, VirtualWord VirtualWord) 
        {
            this.RulesConfig = RulesConfig;
            this.VirtualWord = VirtualWord;
            this.Threads = new List<EngineThread>();

            CreateControllerThread();
            CreateThreads();
        }
    }
}
