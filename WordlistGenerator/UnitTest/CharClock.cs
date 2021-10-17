using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.UnitTest
{
    public class CharClock : IUnitTestCommand
    {
        public void PerformFeatures()
        {
            var collection = new WordlistGenerator.CharCollection("a", new string[] { "a", "b", "c" });
            var clock = new WordlistGenerator.CharClock(collection);

            for (ulong simulation = 0; simulation < 10; simulation++)
            {
                Console.WriteLine($"\n--Step {simulation}--");
                clock.Increment();
                    
                Console.WriteLine($"Increment() EXECUTED!");
                Console.WriteLine($"GetChar() = {clock.GetChar()}");
                Console.WriteLine($"IsEnd() = {clock.IsEnd()}");
                Console.WriteLine($"CheckTurn() = {clock.CheckTurn()}");
                Console.WriteLine($"CheckTurnAndReset() = {clock.CheckTurnAndReset()}");
            }
        }

        public string GetTestDescription()
        {
            return "This UnitTest will test char clock to check incremental functions and loops";
        }

        public string GetTestName()
        {
            return "CharClock";
        }
    }
}
