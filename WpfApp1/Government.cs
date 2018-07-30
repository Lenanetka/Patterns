using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Government
    {
        public string name { get; }
        private Government(string name)
        {
            this.name = name;
        }
        private static Government king;
        private static Government queen;
        private static string generateName(string prefix)
        {
            return prefix + "#" + new Random().Next(1, 10001));
        }
        public static Government getKing()
        {
            if (king == null) king = new Government(generateName("King"));
            return king;
        }
        public static Government getQueen()
        {
            if (queen == null) queen = new Government(generateName("Queen"));
            return queen;
        }
        public static Government newKing()
        {
            king = new Government(generateName("King"));
            return king;
        }
        public static Government newQueen()
        {
            queen = new Government(generateName("Queen"));
            return queen;
        }
    }
}
