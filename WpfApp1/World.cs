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
        public delegate void newGovernment(string king, string queen);
        public event newGovernment OnNewGovernment;
        private void coupDetat()
        {
            king = Government.newKing();
            queen = Government.newQueen();
            OnNewGovernment(king.name, queen.name);
        }
        public delegate void nextAge(int age);
        public event nextAge OnNextAge;
        private void ageIterator(int step)
        {
            age += step;
            OnNextAge(age);
        }
        private int nextStateChance = 0;
        private static WorldState[,] nextState=new WorldState[,]
            //Stagnation,               DarkAge,                CoupDetat - current
        { 
            { WorldState.Stagnation,    WorldState.DarkAge,     WorldState.CoupDetat }, //no changes
            { WorldState.DarkAge,       WorldState.CoupDetat,       WorldState.Stagnation } //next
        };
        public delegate void nextWorldStateEvent(WorldState nextState);
        public event nextWorldStateEvent OnNextWorldState;
        private void nextStateRandom()
        {
            Random rnd = new Random();
            int next = rnd.Next(1, 101);
            if (next <= nextStateChance) next = 0;
            else next = 1;

            nextStateChance += 20;

            state = nextState[next, (int)state];
            if (next == 1)
            {
                nextStateChance = 0;
                OnNextWorldState(state);
                if (state == WorldState.CoupDetat) coupDetat();
            }
        }
    }
}