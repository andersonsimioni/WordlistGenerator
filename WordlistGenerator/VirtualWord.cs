using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordlistGenerator
{
    [Serializable]
    public class VirtualWord
    {
        [JsonProperty("RulesConfig")]
        private readonly VirtualWordRulesConfig RulesConfig;

        [JsonProperty("WordChars")]
        private readonly List<CharClock> WordChars;

        public int Length() 
        {
            return WordChars.Count;
        }

        /// <summary>
        /// Concat string of CharClock combination
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(WordChars.Select(wd => wd.GetChar()));
        }

        /// <summary>
        /// Concat string of inverted CharClock combination
        /// </summary>
        /// <returns></returns>
        public string ToStringInvert() 
        {
            var array = WordChars.ToArray();
            Array.Reverse(array, 0, array.Length);

            return string.Concat(array.Select(wd=>wd.GetChar()));
        }

        /// <summary>
        /// Check if words computation end
        /// </summary>
        /// <returns></returns>
        public bool CanNext() 
        {
            if (RulesConfig.MaxWordSize == 0)
                return true;

            var anyNotEnd = WordChars.Any(wd => wd.IsEnd() == false);
            var allEnd = !anyNotEnd;

            if (allEnd && WordChars.Count() == RulesConfig.MaxWordSize)
                return false;

            return true;
        }

        /// <summary>
        /// Compute next word combination
        /// </summary>
        public void Next() 
        {
            var anyNotEnd = WordChars.Any(wd => wd.IsEnd() == false);
            var allEnd = !anyNotEnd;

            if (allEnd) 
            {
                WordChars.Add(new CharClock(RulesConfig.CharCollection));
            }
            else 
            {
                for (int i = 0; i < WordChars.Count(); i++)
                {
                    WordChars[i].Increment();
                    if (WordChars[i].CheckTurnAndReset() == false)
                        break;
                }
            }
        }

        /// <summary>
        /// Compute next N rounds words combinations,
        /// it use auto CheckNext();
        /// </summary>
        /// <param name="rounds"></param>
        public void Next(int rounds) 
        {
            while(CanNext() && rounds > 0) 
            {
                Next();
                rounds--;
            }   
        }

        public List<CharClock> GetWordChars() 
        {
            return WordChars;
        }

        /// <summary>
        /// Set initial word size based on rules
        /// </summary>
        private void SetInitialWordChars() 
        {
            if (this.RulesConfig.MinWordSize <= 0)
                WordChars.Add(new CharClock(this.RulesConfig.CharCollection));
            else 
            {
                for (int i = 0; i < this.RulesConfig.MinWordSize; i++)
                    WordChars.Add(new CharClock(this.RulesConfig.CharCollection));
            }
        }

        public VirtualWord(VirtualWordRulesConfig rulesConfig) 
        {
            this.WordChars = new List<CharClock>();
            this.RulesConfig = rulesConfig;
            SetInitialWordChars();
        }

        [JsonConstructor]
        public VirtualWord(List<CharClock> WordChars, VirtualWordRulesConfig RulesConfig) 
        {
            this.WordChars = WordChars;
            this.RulesConfig = RulesConfig;
        }
    }
}
