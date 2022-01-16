using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wel.Battle.Game.Core.Entities.Weapons
{
    public class Knife : Weapon
    {
        public Knife()
        {
            Name = GetType().Name;
            Damage = 6;
            Durability = 10;
        }

        public void Stab(Player defender)
        {
            defender.Health -= Damage;
            Durability -= 5;
        }
    }
}
