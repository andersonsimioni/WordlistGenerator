using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordlistGenerator.TerminalCommands
{
    public class NewSession : EngineRunner, ICommand
    {
        public string GetHelp()
        {
            return $"Create new wordlist generator session" +
                   $"\n--threads: threas number count, default is 1" +
                   $"\n--cpWordCount: Checkpoint words count" +
                   $"\n--wdPath: Wordlist save path" +
                   $"\n--minsize: minimum size of word" +
                   $"\n--maxsize: maximum sizeof word" +
                   $"\n--charColl: Character Collection, can be a path or text" +

                   $"\n--maxEqualsAll: maximum all chars types repetition" +
                   $"\n--maxEqualsLower: maximum only lower chars repetition" +
                   $"\n--maxEqualsUpper: maximum only upper chars repetition" +
                   $"\n--maxEqualsSpecial: maximum only special chars repetition" +
                   $"\n--maxEqualsNumeric: maximum only numeric chars repetition" +
                   $"\n--maxEqualsNormal: maximum only normal chars repetition" +

                   $"\n--maxSequencialAll: maximum all chars types sequencial repetition" +
                   $"\n--maxSequencialLower: maximum only lower chars sequencial repetition" +
                   $"\n--maxSequencialUpper: maximum only upper chars sequencial repetition" +
                   $"\n--maxSequencialSpecial: maximum only special chars sequencial repetition" +
                   $"\n--maxSequencialNumeric: maximum only numeric chars sequencial repetition" +
                   $"\n--maxSequencialNormal: maximum only normal chars sequencial repetition";
        }

        public string GetName()
        {
            return "newSession".ToUpper();
        }

        public void Run(string[] args)
        {
            var list = args.Select(c=>c.ToUpper()).ToList();
            var rules = new VirtualWordRulesConfig();

            var threadsIndex = list.IndexOf("--threads".ToUpper()) + 1;
            rules.ThreadsNumber = threadsIndex == 0 ? 1 : int.Parse(args[threadsIndex]);

            var checkpointWordsCountIndex = list.IndexOf("--cpWordCount".ToUpper()) + 1;
            var checkpointPathIndex = list.IndexOf("--cpPath".ToUpper()) + 1;
            var wordlistPathIndex = list.IndexOf("--wdPath".ToUpper()) + 1;
            rules.CheckpointWordsCount = checkpointWordsCountIndex == 0 ? 1000 : int.Parse(args[checkpointWordsCountIndex]);
            rules.WordlistPath = wordlistPathIndex == 0 ? "Wordlist.txt" : args[wordlistPathIndex];


            var minSizeIndex = list.IndexOf("--minsize".ToUpper()) + 1;
            var maxSizeIndex = list.IndexOf("--maxsize".ToUpper()) + 1;
            rules.MinWordSize = minSizeIndex == 0 ? 0 : int.Parse(args[minSizeIndex]);
            rules.MaxWordSize = maxSizeIndex == 0 ? 0 : int.Parse(args[maxSizeIndex]);



            var charCollValIndex = list.IndexOf("--charColl".ToUpper()) + 1;
            var charCollVal = args[charCollValIndex];
            if (File.Exists(charCollVal))
                rules.CharCollection = new CharCollection("default", File.ReadAllText(charCollVal).ToCharArray().Select(c => c.ToString()).ToArray());
            else
                rules.CharCollection = new CharCollection("default", charCollVal.ToCharArray().Select(c => c.ToString()).ToArray());



            var maxEqualsAllIndex = list.IndexOf("--maxEqualsAll".ToUpper()) + 1;
            var maxEqualsLowerIndex = list.IndexOf("--maxEqualsLower".ToUpper()) + 1;
            var maxEqualsUpperIndex = list.IndexOf("--maxEqualsUpper".ToUpper()) + 1;
            var maxEqualsSpecialIndex = list.IndexOf("--maxEqualsSpecial".ToUpper()) + 1;
            var maxEqualsNumericIndex = list.IndexOf("--maxEqualsNumeric".ToUpper()) + 1;
            var maxEqualsNormalIndex = list.IndexOf("--maxEqualsNormal".ToUpper()) + 1;
            rules.MaxEqualsAll = maxEqualsAllIndex == 0 ? 0 : int.Parse(args[maxEqualsAllIndex]);
            rules.MaxEqualsOfLowerChars = maxEqualsLowerIndex == 0 ? 0 : int.Parse(args[maxEqualsLowerIndex]);
            rules.MaxEqualsOfUpperChars = maxEqualsUpperIndex == 0 ? 0 : int.Parse(args[maxEqualsUpperIndex]);
            rules.MaxEqualsOfSpecialChars = maxEqualsSpecialIndex == 0 ? 0 : int.Parse(args[maxEqualsSpecialIndex]);
            rules.MaxEqualsOfNumericChars = maxEqualsNumericIndex == 0 ? 0 : int.Parse(args[maxEqualsNumericIndex]);
            rules.MaxEqualsOfNormalChars = maxEqualsNormalIndex == 0 ? 0 : int.Parse(args[maxEqualsNormalIndex]);



            var maxSequencialAllIndex = list.IndexOf("--maxSequencialAll".ToUpper()) + 1;
            var maxSequencialLowerIndex = list.IndexOf("--maxSequencialLower".ToUpper()) + 1;
            var maxSequencialUpperIndex = list.IndexOf("--maxSequencialUpper".ToUpper()) + 1;
            var maxSequencialSpecialIndex = list.IndexOf("--maxSequencialSpecial".ToUpper()) + 1;
            var maxSequencialNumericIndex = list.IndexOf("--maxSequencialNumeric".ToUpper()) + 1;
            var maxSequencialNormalIndex = list.IndexOf("--maxSequencialNormal".ToUpper()) + 1;
            rules.MaxSequencialRepetitionAll = maxSequencialAllIndex == 0 ? 0 : int.Parse(args[maxSequencialAllIndex]);
            rules.MaxSequencialRepetitionsOfLowerChars = maxSequencialLowerIndex == 0 ? 0 : int.Parse(args[maxSequencialLowerIndex]);
            rules.MaxSequencialRepetitionsOfUpperChars = maxSequencialUpperIndex == 0 ? 0 : int.Parse(args[maxSequencialUpperIndex]);
            rules.MaxSequencialRepetitionsOfSpecialChars = maxSequencialSpecialIndex == 0 ? 0 : int.Parse(args[maxSequencialSpecialIndex]);
            rules.MaxSequencialRepetitionsOfNumericChars = maxSequencialNumericIndex == 0 ? 0 : int.Parse(args[maxSequencialNumericIndex]);
            rules.MaxSequencialRepetitionsOfNormalChars = maxSequencialNormalIndex == 0 ? 0 : int.Parse(args[maxSequencialNormalIndex]);


            RunEngine(new Engine(rules));
        }
    }
}
