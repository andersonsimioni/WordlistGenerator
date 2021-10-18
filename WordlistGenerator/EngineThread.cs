using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordlistGenerator
{
    public class EngineThread
    {
        private readonly int ThreadId;
        private readonly int ThreadsCount;

        private readonly VirtualWordRulesConfig RulesConfig;
        private readonly VirtualWord VirtualWord;

        private bool EndStatus;
        private bool RunStatus;
        private Thread WordGeneratorProcess;
        private readonly List<string> GeneratedWords;
        private readonly List<WordRules.IRule> Rules;

        public VirtualWord GetVirtualWord() 
        {
            return this.VirtualWord;
        }

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

                        if (VirtualWord.CanNext())
                            VirtualWord.Next((ulong)ThreadsCount);
                        else
                            EndProcess();
                    }
                }
            });
        }

        public EngineThread(int threadId, int threadsCount, VirtualWordRulesConfig rulesConfig, VirtualWord virtualWord) 
        {
            this.GeneratedWords = new List<string>();
            this.Rules = new List<WordRules.IRule>();

            this.RulesConfig = rulesConfig;

            this.VirtualWord = JsonConvert.DeserializeObject<VirtualWord>(JsonConvert.SerializeObject(virtualWord)); //Deep copy

            if(threadId > 0)
                this.VirtualWord.Next((ulong)threadId);

            this.ThreadId = threadId;
            this.ThreadsCount = threadsCount;

            LoadAllRules();
            CreateThreadProcess();
        }
    }
}
