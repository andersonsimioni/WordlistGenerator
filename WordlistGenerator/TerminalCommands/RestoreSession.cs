using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.TerminalCommands
{
    public class RestoreSession : EngineRunner, ICommand
    {
        public string GetHelp()
        {
            return "Restore old session";
        }

        public string GetName()
        {
            return "restoreSession".ToUpper();
        }

        public void Run(string[] args)
        {
            var checkpoint = new Checkpoint(VirtualWordRulesConfig.CheckpointPath);
            checkpoint.RestorePoint();

            Console.WriteLine($"Checkpoint restored word: {checkpoint.GetData().GetLastWord()}");

            RunEngine(checkpoint.GetData());
        }
    }
}
