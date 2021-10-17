using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlistGenerator.WordRules
{
    /// <summary>
    /// Interface to call rule verification function for all rules
    /// </summary>
    public interface IRule
    {
        public bool CheckRule(VirtualWord wordChars);
    }
}
