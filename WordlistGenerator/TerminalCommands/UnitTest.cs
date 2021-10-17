using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.TerminalCommands
{
    public class UnitTest : ICommand
    {
        public string GetHelp()
        {
            return "Run unit test list to check if program working correct";
        }

        public string GetName()
        {
            return "unitTest".ToUpper();
        }

        public void Run(string[] args)
        {
            WordlistGenerator.UnitTest.Main.Run();
        }
    }
}
