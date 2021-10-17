using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.UnitTest
{
    public class CharCollection : IUnitTestCommand
    {
        public void PerformFeatures()
        {
            var collection = new WordlistGenerator.CharCollection("a", new string[] { "a", "b", "c" });

            for (ulong simulation = 0; simulation < collection.Length() * 2; simulation++)
            {
                var charSelection = collection.GetChar(simulation);
                var turnCheck = collection.CheckTurn(simulation);
                var endCheck = collection.IsEnd(simulation);

                Console.WriteLine($"\ncharSelection({simulation}) = {charSelection}");
                Console.WriteLine($"checkTurn({simulation}) = {turnCheck}");
                Console.WriteLine($"endCheck({simulation}) = {endCheck}");
            }
        }

        public string GetTestDescription()
        {
            return "Test all functionalities of char collection";
        }

        public string GetTestName()
        {
            return "CharCollection";
        }
    }
}
