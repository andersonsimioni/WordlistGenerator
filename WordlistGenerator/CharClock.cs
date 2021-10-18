using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WordlistGenerator
{
    [Serializable]
    public class CharClock
    {
        [Flags]
        public enum CharTypes
        {
            LOWER = 1,
            UPPER = 2,
            NORMAL = 4,
            NUMERIC = 8,
            SPECIAL = 16,
        }

        private readonly string Numeric = "0123456789";
        private readonly string NormalLower = "qwertyuiopasdfghjklçzxcvbnm";
        private readonly string NormalUpper = "QWERTYUIOPASDFGHJKLÇZXCVBNM";
        private readonly string Special = "'\"!@#$%¨&*()-_=+§`´[{ªº]}~^?/°;:.><,";

        [JsonProperty("CurrentIndex")]
        private ulong CurrentIndex;
        [JsonProperty("CharCollection")]
        private readonly CharCollection CharCollection;

        public override string ToString()
        {
            return GetChar();
        }

        public void SetCurrentIndex(ulong newIndex) 
        {
            this.CurrentIndex = newIndex;
        }

        public CharCollection GetCharCollection() 
        {
            return this.CharCollection;
        }

        public ulong GetCurrentIndex() 
        {
            return this.CurrentIndex;
        }

        public CharTypes GetCharType() 
        {
            if (IsNormalLower())
                return CharTypes.LOWER | CharTypes.NORMAL;
            if (IsNormalUpper())
                return CharTypes.UPPER | CharTypes.NORMAL;
            if (IsSpecial())
                return CharTypes.SPECIAL;
            if (IsNumeric())
                return CharTypes.NUMERIC;

            return CharTypes.NORMAL;
        }

        public bool IsNumeric() 
        {
            return Numeric.Contains(GetChar());
        }

        public bool IsSpecial() 
        {
            return Special.Contains(GetChar());
        }

        public bool IsNormal()
        {
            return NormalLower.Contains(GetChar()) || NormalUpper.Contains(GetChar());
        }

        public bool IsNormalUpper()
        {
            return NormalUpper.Contains(GetChar());
        }

        public bool IsNormalLower()
        {
            return NormalLower.Contains(GetChar());
        }

        public string GetChar() 
        {
            return this.CharCollection.GetChar(CurrentIndex);
        }
        
        public bool IsEnd() 
        {
            return this.CharCollection.IsEnd(CurrentIndex);
        }

        public void Increment(ulong units = 1) 
        {
            this.CurrentIndex += units;
        }

        public CharClock(CharCollection charCollection, ulong salt = 0) 
        {
            if (charCollection == null)
                throw new Exception("Char clock have charset null");

            this.CharCollection = charCollection;
            this.CurrentIndex = salt;
        }

        [JsonConstructor]
        public CharClock(ulong CurrentIndex, CharCollection CharCollection) 
        {
            this.CurrentIndex = CurrentIndex;
            this.CharCollection = CharCollection;
        }
    }
}
