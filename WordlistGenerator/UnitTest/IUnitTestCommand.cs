using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.UnitTest
{
    interface IUnitTestCommand
    {
        public void PerformFeatures();

        public void RunTest() 
        {
            try
            {
                Console.WriteLine($"\n\n### Starting {GetTestName()} UnitTest ###");

                PerformFeatures();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccess on execute {GetTestName()} UnitTest");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nFail on execute {GetTestName()} UnitTest");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        public string GetTestName();

        public string GetTestDescription();
    }
}
