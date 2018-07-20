using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Singleton, State*/
//World can change it's state: economic crisis, golden age, dark age, period of stagnation, coup d'etat
//Generating evants depends on current state

namespace Patterns
{
    enum WorldState
    {
        Stagnation, EconomicCrisis, GoldenAge, DarkAge, CoupDetat
    }
    class World
    {
        int age = 0;
        WorldState state = WorldState.Stagnation;
        List<Hamster> hamsters;
        List<Hamster> cemetery;
    }
}
