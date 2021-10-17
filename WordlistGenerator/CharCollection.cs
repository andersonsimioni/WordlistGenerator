using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordlistGenerator
{
    [Serializable]
    public class CharCollection
    {
        [JsonProperty("Name")]
        private readonly string Name;
        [JsonProperty("Chars")]
        private readonly string[] Chars;

        public override string ToString()
        {
            return $"-{Name} {string.Concat(Chars)}";
        }

        public ulong Length() 
        {
            return (ulong)Chars.Length;
        }

        public bool CheckTurn(ulong index) 
        {
            var len = Length();
            var rest = index % len;

            return rest == 0;
        }

        public bool IsEnd(ulong index)
        {
            var len = Length();
            var rest = index % len;

            return rest == len - 1;
        }

        public string GetChar(ulong index) 
        {
            var len = Length();
            var rest = index % len;

            return Chars[rest];
        }

        public string GetName() 
        {
            return this.Name;
        }

        public string[] GetChars() 
        {
            return this.Chars;
        }

        [JsonConstructor]
        public CharCollection(string Name, string[] Chars) 
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception("Char Collection name is null");
            if (Chars.Length == 0)
                throw new Exception("Chars list is empty");

            this.Name = Name;
            this.Chars = Chars;
        }
    }
}
