using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Entities.Weapons;
using Wel.Battle.Game.Core.Services;

namespace Wel.Battle.Game.Core.Entities.Classes
{
    public class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            Health = 240;
            AttackStrength = 40;
            DefenseStrength = 60;
        }

        public static void FireBall(Player defender)
        {
            defender.Health -= 20;
        }

        public void Heal(Player friendly)
        {
            Health -= 10;
            friendly.Health += 10;
        }
    }
}