using System;
using System.Threading.Tasks;
using System.Threading;


namespace WordlistGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "newSession", "--threads", "4", "--minSize", "1", "--maxSize", "12", "--charColl", "ab", "--cpWordCount", "1000000" };
            //args = new string[] { "restoreSession" };
            //args = new string[] { "unitTest" };

            new GeneratorTerminalManager(args);
        }
    }
}
