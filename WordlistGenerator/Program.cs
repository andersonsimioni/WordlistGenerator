using System;
using System.Threading.Tasks;
using System.Threading;


namespace WordlistGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "newSession", "--maxSequencialNormal", "2", "--minSize", "1", "--maxSize", "12", "--charColl", "abAB12@!" };
            //args = new string[] { "restoreSession" };

            new GeneratorTerminalManager(args);
        }
    }
}
