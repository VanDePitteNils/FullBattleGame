using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wel.Battle.Game.Core.Entities.Weapons
{
    public abstract class Weapon
    {
        public int Damage { get; set; }
        public int Durability { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value.Trim()))
                {
                    throw new NullReferenceException("Gelieve een wapen naam op te geven");
                }
                name = GetType().Name;
            }
        }

        public Weapon(string name)
        {
            Name = name;
            Damage = 5;
            Durability = 10;
        }

        public override string ToString()
        {
            return $"{Name} - {Durability}";
        }
    }
}
