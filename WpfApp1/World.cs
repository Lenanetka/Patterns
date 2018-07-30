using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Singleton, State*/

namespace Patterns
{
    public enum WorldState
    {
        Stagnation, DarkAge, CoupDetat
    }
    public class World
    {
        int age;
        WorldState state = WorldState.Stagnation;
        private static World instance;
        private static Government king;
        private static Government queen;
        private World()
        {
            age = 0;
            state = WorldState.Stagnation;
            king = Government.getKing();
            queen = Government.getQueen();
        }
        public static World Instance()
        {
            if (instance == null) instance = new World();
            return instance;
        }
        public void nextYear()
        {
            ageIterator(10);
            nextStateRandom();
        }

        public delegate void OnRandomEvent (string message);
        public event OnRandomEvent newRandomEvent;
        private void randomEvent()
        {
            string[,] events = new string[,] { 
            { "Погода хорошая", "Популяция хомячков возросла", "Оглашение всемирного дня добра" },
            { "Случился всемирный потоп", "Грянул гром", "Уровень преступности возрос" },
            { "Критический уровень недовольства населения", "Подпольная организация вступила в дело", "Правительство бежало" }
            };
            newRandomEvent(events[(int)state, new Random().Next(0, 2)]);
        }
        public delegate void OnNewGovernment(string king, string queen);
        public event OnNewGovernment newGovernmentEvent;
        private void coupDetat()
        {
            king = Government.newKing();
            queen = Government.newQueen();
            newGovernmentEvent(king.name, queen.name);
        }
        public delegate void OnNextAge(int age);
        public event OnNextAge nextAgeEvent;
        private void ageIterator(int step)
        {
            age += step;
            nextAgeEvent(age);
        }
        private int nextStateChance = 0;
        private static WorldState[,] nextState=new WorldState[,]
            //Stagnation,               DarkAge,                CoupDetat - current
        { 
            { WorldState.Stagnation,    WorldState.DarkAge,     WorldState.CoupDetat }, //no changes
            { WorldState.DarkAge,       WorldState.CoupDetat,       WorldState.Stagnation } //next
        };
        public delegate void OnNextWorldState(WorldState nextState);
        public event OnNextWorldState nextWorldStateEvent;
        private void nextStateRandom()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int next = rnd.Next(1, 101);
            if (next <= nextStateChance) next = 1;
            else next = 0;
            
            state = nextState[next, (int)state];
            if (next == 1)
            {
                nextStateChance = 0;
                nextWorldStateEvent(state);
                if (state == WorldState.CoupDetat) coupDetat();
            }
            switch (state)
            {
                case WorldState.Stagnation:
                    nextStateChance += 10;
                    break;
                case WorldState.DarkAge:
                    nextStateChance += 25;
                    break;
                case WorldState.CoupDetat:
                    nextStateChance += 100;
                    break;
            }

            randomEvent();
        }
    }
}