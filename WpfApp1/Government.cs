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
        public static Government getKing()
        {
            if (king == null) king = new Government("King#" + new Random().Next(1, 10001));
            return king;
        }
        public static Government getQueen()
        {
            if (queen == null) queen = new Government("Queen#" + new Random().Next(1, 10001));
            return queen;
        }
        public static Government newKing()
        {
            king = new Government("King#" + new Random().Next(1, 10001));
            return king;
        }
        public static Government newQueen()
        {
            queen = new Government("Queen#" + new Random().Next(1, 10001));
            return queen;
        }
    }
}
