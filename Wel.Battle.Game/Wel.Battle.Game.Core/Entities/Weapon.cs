using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Entities;

namespace Wel.Battle.Game.Core.Entities.Weapons
{
    public abstract class Weapon
    {
        public int Damage { get; set; }
        public int Durability { get; set; }
        public bool Equiped { get; set; }

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

        public Weapon()
        {
            Name = GetType().Name;
            Damage = 5;
            Durability = 10;
        }

        public override string ToString()
        {
            if (Equiped)
            {
                return $"{Name} - Durability: {Durability} (E)";
            }
            return $"{Name} - Durability: {Durability}";
        }
    }
}
