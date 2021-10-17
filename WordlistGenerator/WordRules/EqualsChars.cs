using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.WordRules
{
    public class EqualsChars : IRule
    {
        private readonly VirtualWordRulesConfig RulesConfig;

        /// <summary>
        /// Check if word have correct equals chars config
        /// </summary>
        /// <returns></returns>
        public bool CheckRule(VirtualWord word)
        {
            var wordChars = word.GetWordChars();
            var all = wordChars.GroupBy(wd => wd.GetChar());
            var normal = wordChars.Where(wd => wd.IsNormal()).GroupBy(wd => wd.GetChar());
            var special = wordChars.Where(wd => wd.IsSpecial()).GroupBy(wd => wd.GetChar());
            var numeric = wordChars.Where(wd => wd.IsNumeric()).GroupBy(wd => wd.GetChar());
            var lower = wordChars.Where(wd => wd.IsNormalLower()).GroupBy(wd => wd.GetChar());
            var upper = wordChars.Where(wd => wd.IsNormalUpper()).GroupBy(wd => wd.GetChar());

            if (RulesConfig.MaxEqualsAll > 0 && all.Any(l => l.Count() > RulesConfig.MaxEqualsAll))
                return false;
            if (RulesConfig.MaxEqualsOfLowerChars > 0 && lower.Any(l => l.Count() > RulesConfig.MaxEqualsOfLowerChars))
                return false;
            if (RulesConfig.MaxEqualsOfUpperChars > 0 && upper.Any(l => l.Count() > RulesConfig.MaxEqualsOfUpperChars))
                return false;
            if (RulesConfig.MaxEqualsOfNormalChars > 0 && normal.Any(l => l.Count() > RulesConfig.MaxEqualsOfNormalChars))
                return false;
            if (RulesConfig.MaxEqualsOfSpecialChars > 0 && special.Any(l => l.Count() > RulesConfig.MaxEqualsOfSpecialChars))
                return false;
            if (RulesConfig.MaxEqualsOfNumericChars > 0 && numeric.Any(l => l.Count() > RulesConfig.MaxEqualsOfNumericChars))
                return false;

            return true;
        }

        public EqualsChars(VirtualWordRulesConfig rulesConfig)
        {
            if (rulesConfig == null)
                throw new Exception("Virtual Word Rules Config is null");

            this.RulesConfig = rulesConfig;
        }
    }
}
