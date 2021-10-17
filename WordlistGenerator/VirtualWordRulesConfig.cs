using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator
{
    public class VirtualWordRulesConfig
    {
        public static readonly string CheckpointPath = "Checkpoint";
        public string WordlistPath { get; set; }
        public int CheckpointWordsCount { get; set; }



        public int MinWordSize { get; set; }
        public int MaxWordSize { get; set; }



        public int MaxEqualsAll { get; set; }
        public int MaxEqualsOfNumericChars { get; set; }
        public int MaxEqualsOfLowerChars { get; set; }
        public int MaxEqualsOfUpperChars { get; set; }
        public int MaxEqualsOfNormalChars { get; set; }
        public int MaxEqualsOfSpecialChars { get; set; }


        public int MaxSequencialRepetitionAll { get; set; }
        public int MaxSequencialRepetitionsOfNumericChars { get; set; }
        public int MaxSequencialRepetitionsOfLowerChars { get; set; }
        public int MaxSequencialRepetitionsOfUpperChars { get; set; }
        public int MaxSequencialRepetitionsOfNormalChars { get; set; }
        public int MaxSequencialRepetitionsOfSpecialChars { get; set; }

        
        public CharCollection CharCollection { get; set; }
    }
}
