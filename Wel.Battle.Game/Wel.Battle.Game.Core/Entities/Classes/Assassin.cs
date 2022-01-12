using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Entities.Weapons;

namespace Wel.Battle.Game.Core.Entities.Classes
{
    public class Assassin : Player
    {
        public Assassin(string name) : base(name)
        {
            Health = 120;
            AttackStrength = 50;
            DefenseStrength = 5;
        }

        public static void Stab(Player defender)
        {
            defender.Health -= 30;

        }
    }
}
