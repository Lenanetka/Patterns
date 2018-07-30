using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    class LogBuilder
    {
        public LogBuilder()
        {

        }
        private int currentAge = 0;
        public void setCurrentAge(int age)
        {
            currentAge = age;
        }
        public delegate void newEraEvent(string message);
        public event newEraEvent OnNewEraEvent;
        public void newEra(WorldState nextState)
        {
            string message = currentAge.ToString("D4") + ": Новая эра: ";
            switch (nextState)
            {
                case WorldState.Stagnation: message += "Всё спокойно"; break;
                case WorldState.DarkAge: message += "Тёмные века"; break;
                case WorldState.CoupDetat: message += "Государственный переворот"; break;
            }
            OnNewEraEvent(message);
        }        
    }
}
