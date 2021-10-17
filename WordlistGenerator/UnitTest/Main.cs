using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.UnitTest
{
    public class Main
    {
        public static void Run()
        {
            var tests = new UnitTest.IUnitTestCommand[]{
                new UnitTest.CharCollection(),
                new UnitTest.CharClock(),
                new UnitTest.VirtualWord(),
            };

            foreach (var test in tests)
                test.RunTest();
        }
    }
}
