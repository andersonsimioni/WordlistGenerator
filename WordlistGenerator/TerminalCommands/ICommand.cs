using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.TerminalCommands
{
    public interface ICommand
    {
        public string GetName();

        public string GetHelp();

        public void Run(string[] args);
    }
}
