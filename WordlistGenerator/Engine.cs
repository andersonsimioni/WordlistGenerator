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
        private readonly VirtualWord VirtualWord;

        private readonly List<WordRules.IRule> Rules;

        private bool EndStatus;
        private bool RunStatus;
        private Thread WordGeneratorProcess;
        private readonly List<string> GeneratedWords;

        /// <summary>
        /// Get last computed word
        /// </summary>
        /// <returns></returns>
        public string GetLastWord() 
        {
            return this.VirtualWord.ToString();
        }

        /// <summary>
        /// Check if all rules are respected
        /// </summary>
        /// <returns></returns>
        private bool CheckRules() 
        {
            foreach (var rule in Rules)
                if (rule.CheckRule(VirtualWord) == false)
                    return false;

            return true;
        }

        /// <summary>
        /// Load all rules verification functions
        /// </summary>
        private void LoadAllRules() 
        {
            Rules.Add(new WordRules.WordSize(RulesConfig));
            Rules.Add(new WordRules.EqualsChars(RulesConfig));
            Rules.Add(new WordRules.SequencialRepetitionChars(RulesConfig));
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
            this.GeneratedWords.Clear();
        }

        /// <summary>
        /// Return how much words are generated
        /// </summary>
        /// <returns></returns>
        public int GetGeneratedWordsCount() 
        {
            return this.GeneratedWords.Count();
        }

        /// <summary>
        /// Return current genetared words list
        /// </summary>
        /// <returns></returns>
        public string[] GetGeneratedWords() 
        {
            return this.GeneratedWords.ToArray();
        }

        /// <summary>
        /// Start words computation thread
        /// </summary>
        public void StartProcess()
        {
            this.RunStatus = true;
            this.WordGeneratorProcess.IsBackground = true;
            this.WordGeneratorProcess.Start();
        }

        /// <summary>
        /// End words computation thread
        /// </summary>
        public void EndProcess() 
        {
            this.EndStatus = true;
        }

        /// <summary>
        /// Pause thread execution
        /// </summary>
        public void Pause() 
        {
            this.RunStatus = false;
        }

        /// <summary>
        /// Resume thread execution
        /// </summary>
        public void Resume() 
        {
            this.RunStatus = true;
        }

        /// <summary>
        /// Save all current generated words
        /// </summary>
        private void SaveWords() 
        {
            File.AppendAllLines(RulesConfig.WordlistPath, GeneratedWords);    
        }

        /// <summary>
        /// Save current word list and create engine checkpoint
        /// to restore in future
        /// </summary>
        private void SaveCheckpoint() 
        {
            var checkpoint = new Checkpoint(this, VirtualWordRulesConfig.CheckpointPath);
            checkpoint.CreatePoint();

            SaveWords();
            ClearGeneratedWords();
        }

        /// <summary>
        /// Set function for computation words thread
        /// </summary>
        private void CreateThreadProcess() 
        {
            this.WordGeneratorProcess = new Thread(() => 
            {
                while (this.EndStatus == false)
                {
                    if (this.RunStatus == false)
                        Thread.Sleep(1000);
                    else 
                    {
                        if (CheckRules())
                            GeneratedWords.Add(VirtualWord.ToString());

                        if (GeneratedWords.Count >= RulesConfig.CheckpointWordsCount)
                            SaveCheckpoint();

                        if (VirtualWord.CanNext())
                            VirtualWord.Next();
                        else
                            EndProcess();
                    }
                }

                SaveCheckpoint();
            });
        }

        public Engine(VirtualWordRulesConfig rulesConfig) 
        {
            this.RulesConfig = rulesConfig;
            this.VirtualWord = new VirtualWord(rulesConfig);
            this.GeneratedWords = new List<string>();
            this.Rules = new List<WordRules.IRule>();
            LoadAllRules();

            CreateThreadProcess();
        }

        [JsonConstructor]
        public Engine(VirtualWordRulesConfig RulesConfig, VirtualWord VirtualWord) 
        {
            this.RulesConfig = RulesConfig;
            this.VirtualWord = VirtualWord;
            this.GeneratedWords = new List<string>();
            this.Rules = new List<WordRules.IRule>();
            LoadAllRules();

            CreateThreadProcess();
        }
    }
}
