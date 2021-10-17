using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordlistGenerator
{
    public class GeneratorTerminalManager
    {
        private readonly TerminalCommands.ICommand[] Commands = new TerminalCommands.ICommand[] 
        {
            new TerminalCommands.UnitTest(),
            new TerminalCommands.NewSession(),
            new TerminalCommands.RestoreSession(),
        };

        private void RunCommand(string name, string[] args) 
        {
            try
            {
                var command = Commands.FirstOrDefault(c => c.GetName() == name);
                command.Run(args);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{name} error on execution");
                Console.WriteLine($"{ex.Message}");
                Console.ResetColor();
            }
        }

        public GeneratorTerminalManager(params string[] args) 
        {
            var commandName = args[0].ToUpper();
            if (commandName == "HELP")
            {
                foreach (var c in Commands)
                {
                    Console.WriteLine($"### {c.GetName()} ###\n");
                    Console.WriteLine(c.GetHelp());
                    Console.WriteLine("\n");
                }
                return;
            }

            if (Commands.Any(c => commandName == c.GetName()))
                RunCommand(commandName, args);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{commandName} Command not found");
                Console.ResetColor();
            }
        }
    }
}
