using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.TerminalCommands
{
    public abstract class EngineRunner
    {
        private void ExecuteSelectedOption(Engine engine) 
        {
            var option = Console.ReadLine().ToUpper();

            if (engine.IsEnd())
                return;

            switch (option)
            {
                case "P":
                    engine.Pause();
                    Console.WriteLine("Wordlist Generator paused");
                    break;
                case "R":
                    engine.Resume();
                    Console.WriteLine("Wordlist Generator resumed");
                    break;
                case "E":
                    engine.EndProcess();
                    Console.WriteLine("Wordlist Generator ended and saved checkpoint");
                    break;
                default:
                    break;
            }
        }

        private void RenderOptions() 
        {
            Console.WriteLine("[p]-pause | [r]-resume | [e]-end");
        }

        public void RunEngine(Engine engine) 
        {
            engine.StartProcess();
            while (engine.IsEnd() == false)
            {
                RenderOptions();
                ExecuteSelectedOption(engine);
            }
        }
    }
}
