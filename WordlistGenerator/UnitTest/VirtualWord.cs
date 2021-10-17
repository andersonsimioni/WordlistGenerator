using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.UnitTest
{
    public class VirtualWord : IUnitTestCommand
    {
        public void PerformFeatures()
        {
            var word = new WordlistGenerator.VirtualWord(new VirtualWordRulesConfig() { 
                MinWordSize = 2,
                MaxWordSize = 3,
                CharCollection = new WordlistGenerator.CharCollection("OH HELL", new string[] { "a", "b" }),
            });

            while (true)
            {
                var txt = word.ToString();
                Console.WriteLine(txt);

                if (word.CanNext())
                    word.Next();
                else
                    break;
            }
        }

        public string GetTestDescription()
        {
            return "Test if virtual word can increment correct chars";
        }

        public string GetTestName()
        {
            return "VirtualWord";
        }
    }
}
