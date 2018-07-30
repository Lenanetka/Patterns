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
        public delegate void OnNewEraEvent(string message);
        public event OnNewEraEvent newEraEvent;
        public void newEra(WorldState nextState)
        {
            string message = currentAge.ToString("D4") + " г.: *Новая эра*: ";
            switch (nextState)
            {
                case WorldState.Stagnation: message += "Застой"; break;
                case WorldState.DarkAge: message += "Тёмные века"; break;
                case WorldState.CoupDetat: message += "Государственный переворот"; break;
            }
            newEraEvent(message);
        }
        public delegate void OnRandomEvent(string message);
        public event OnRandomEvent newRandomEvent;
        public void randomEvent(string eventmessage)
        {
            string message = currentAge.ToString("D4") + " г.: Новости: " + eventmessage;
            newRandomEvent(message);
        }
        public delegate void OnNewGovernment(string message);
        public event OnNewGovernment newGovernmentEvent;
        public void newGovernment(string king, string queen)
        {
            string message = currentAge.ToString("D4") + " г.: Новое правительство: король " + king + " и королева " + queen;
            newGovernmentEvent(message);
        }
    }
}
