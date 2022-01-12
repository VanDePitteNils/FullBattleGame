using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Entities.Weapons;

namespace Wel.Battle.Game.Core.Entities.Classes
{
    public class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            AttackStrength = 60;
            Health = 140;
            DefenseStrength = 175;
        }

        public static void FireBall(Player defender)
        {
            defender.Health -= 20;
        }
    }
}