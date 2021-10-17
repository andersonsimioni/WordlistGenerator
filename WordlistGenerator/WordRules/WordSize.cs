using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.WordRules
{
    public class WordSize : IRule
    {
        private readonly VirtualWordRulesConfig VirtualWordRulesConfig;

        /// <summary>
        /// Check if word is inside of size rules
        /// </summary>
        /// <returns></returns>
        public bool CheckRule(VirtualWord word)
        {
            if (VirtualWordRulesConfig.MinWordSize > 0 && word.Length() < VirtualWordRulesConfig.MinWordSize)
                return false;

            if (VirtualWordRulesConfig.MaxWordSize > 0 && word.Length() > VirtualWordRulesConfig.MaxWordSize)
                return false;

            return true;
        }

        public WordSize(VirtualWordRulesConfig virtualWordRulesConfig) 
        {
            if (virtualWordRulesConfig == null)
                throw new Exception("Virtual Word Rules Config is null");

            this.VirtualWordRulesConfig = virtualWordRulesConfig;
        }
    }
}
