using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class Government
    {
        public string name;
        private Government(string name)
        {

        }
        private static Government king;
        private static Government queen;
        public Government getKing()
        {
            if (king == null) king = new Government("King#" + new Random().Next(1, 10001));
            return king;
        }
        public Government getQueen()
        {
            if (queen == null) queen = new Government("Queen#" + new Random().Next(1, 10001));
            return queen;
        }
    }
}
