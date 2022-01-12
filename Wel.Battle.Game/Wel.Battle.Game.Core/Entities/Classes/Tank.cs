﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wel.Battle.Game.Core.Entities.Classes
{
    public class Tank : Player
    {

        public Tank(string name) : base(name)
        {
            Health = 200;
            AttackStrength = 60;
            DefenseStrength = 20;
        }

        public void Beserk(Player defender)
        {
            defender.Health -= 40;

            AttackStrength += 20;
            DefenseStrength -= 20;
        }
    }
}
