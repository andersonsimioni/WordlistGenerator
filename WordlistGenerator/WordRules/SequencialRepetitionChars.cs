using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.WordRules
{
    public class SequencialRepetitionChars : IRule
    {
        private readonly VirtualWordRulesConfig RulesConfig;

        private bool CheckSequenceRespect(Dictionary<CharClock.CharTypes, int> sequences) 
        {
            if (RulesConfig.MaxSequencialRepetitionAll > 0)
                if (sequences.Any(s => s.Value > RulesConfig.MaxSequencialRepetitionAll))
                    return false;

            if (RulesConfig.MaxSequencialRepetitionsOfLowerChars > 0)
                if (sequences[CharClock.CharTypes.LOWER] > RulesConfig.MaxSequencialRepetitionsOfLowerChars)
                    return false;

            if (RulesConfig.MaxSequencialRepetitionsOfUpperChars > 0)
                if (sequences[CharClock.CharTypes.UPPER] > RulesConfig.MaxSequencialRepetitionsOfUpperChars)
                    return false;

            if (RulesConfig.MaxSequencialRepetitionsOfNormalChars > 0)
                if (sequences[CharClock.CharTypes.NORMAL] > RulesConfig.MaxSequencialRepetitionsOfNormalChars)
                    return false;

            if (RulesConfig.MaxSequencialRepetitionsOfSpecialChars > 0)
                if (sequences[CharClock.CharTypes.SPECIAL] > RulesConfig.MaxSequencialRepetitionsOfSpecialChars)
                    return false;

            if (RulesConfig.MaxSequencialRepetitionsOfNumericChars > 0)
                if (sequences[CharClock.CharTypes.NUMERIC] > RulesConfig.MaxSequencialRepetitionsOfNumericChars)
                    return false;

            return true;
        }

        private void AddFlagCount(Dictionary<CharClock.CharTypes, int> sequences, CharClock.CharTypes charTypes) 
        {
            if (charTypes.HasFlag(CharClock.CharTypes.LOWER))
                if (sequences[CharClock.CharTypes.LOWER] == 0)
                    sequences[CharClock.CharTypes.LOWER] += 2;
                else
                    sequences[CharClock.CharTypes.LOWER]++;

            if (charTypes.HasFlag(CharClock.CharTypes.UPPER))
                if (sequences[CharClock.CharTypes.UPPER] == 0)
                    sequences[CharClock.CharTypes.UPPER] += 2;
                else
                    sequences[CharClock.CharTypes.UPPER]++;

            if (charTypes.HasFlag(CharClock.CharTypes.NORMAL))
                if (sequences[CharClock.CharTypes.NORMAL] == 0)
                    sequences[CharClock.CharTypes.NORMAL] += 2;
                else
                    sequences[CharClock.CharTypes.NORMAL]++;

            if (charTypes.HasFlag(CharClock.CharTypes.SPECIAL))
                if (sequences[CharClock.CharTypes.SPECIAL] == 0)
                    sequences[CharClock.CharTypes.SPECIAL] += 2;
                else
                    sequences[CharClock.CharTypes.SPECIAL]++;

            if (charTypes.HasFlag(CharClock.CharTypes.NUMERIC))
                if (sequences[CharClock.CharTypes.NUMERIC] == 0)
                    sequences[CharClock.CharTypes.NUMERIC] += 2;
                else
                    sequences[CharClock.CharTypes.NUMERIC]++;
        }

        /// <summary>
        /// Check if word contains sequencial chars repetition,
        /// like AAA13 or abcccc123
        /// </summary>
        /// <param name="wordChars"></param>
        /// <returns></returns>
        public bool CheckRule(VirtualWord word)
        {
            var txt = word.ToString();
            var maxSequenceAll = RulesConfig.MaxSequencialRepetitionAll;
            var sequences = new Dictionary<CharClock.CharTypes, int>();
            sequences.Add(CharClock.CharTypes.LOWER, 0);
            sequences.Add(CharClock.CharTypes.UPPER, 0);
            sequences.Add(CharClock.CharTypes.NORMAL, 0);
            sequences.Add(CharClock.CharTypes.NUMERIC, 0);
            sequences.Add(CharClock.CharTypes.SPECIAL, 0);

            var lastChar = string.Empty;
            var charList = word.GetWordChars();
            for (int i = 0; i < charList.Count; i++)
            {
                var thisChar = charList[i].GetChar();
                if (lastChar == string.Empty)
                {
                    lastChar = thisChar;
                    continue;
                }

                if (thisChar == lastChar)
                    AddFlagCount(sequences, charList[i].GetCharType());
                else
                    sequences = sequences.ToDictionary(k => k.Key, v => 0);

                lastChar = thisChar;

                if (CheckSequenceRespect(sequences) == false)
                    return false;
            }

            return true;
        }

        public SequencialRepetitionChars(VirtualWordRulesConfig rulesConfig)
        {
            if (rulesConfig == null)
                throw new Exception("Virtual Word Rules Config is null");

            this.RulesConfig = rulesConfig;
        }
    }
}
