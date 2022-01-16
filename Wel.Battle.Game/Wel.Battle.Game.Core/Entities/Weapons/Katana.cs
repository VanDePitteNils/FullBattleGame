using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wel.Battle.Game.Core.Entities.Weapons
{
    public class Katana : Weapon
    {
        public Katana()
        {
            Name = GetType().Name;
            Damage = 8;
            Durability = 15;
        }

        public void QuickSlash(Player defender)
        {
            defender.Health -= Damage;
            Durability -= 5;
        }
    }
}
