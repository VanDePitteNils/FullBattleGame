using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Interfaces;
using Wel.Battle.Game.Core.Entities.Weapons;

namespace Wel.Battle.Game.Core.Entities
{
    public class Player : IPlayer
    {
        public List<IInventoryItem> Inventory { get; }
        public bool IsAlive { get; set; }
        public bool HasWeapon { get; set; }
        private readonly Random rnd = new Random();

        private int attackStrength;
        public int AttackStrength
        {
            get 
            {
                if(attackStrength >= 200)
                {
                    return 200;
                }
                return attackStrength;
            }
            set { attackStrength = value; }
        }

        private int defenseStrength;
        public int DefenseStrength
        {
            get 
            {
                if (defenseStrength <= 0)
                {
                    defenseStrength = 0;
                }
                return defenseStrength;
            }
            set { defenseStrength = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Gelieve een naam op te geven");
                }
                else if(value.Length > 20)
                {
                    name = value.Trim().Substring(0, 20);
                }
                name = value.Trim();
            }
        }

        private int health;
        public int Health
        {
            get
            {
                if (health <= 0)
                {
                    IsAlive = false;
                    return 0;
                }
                return health;
            }
            set
            {
                health = value;
            }
        }

        public Player(string name)
        {
            Name = name;
            Health = 100;
            IsAlive = true;
            HasWeapon = false;
            AttackStrength = rnd.Next(5,15);
            DefenseStrength = 10;
        }

        public void Equip(Weapon item)
        {
            AttackStrength += item.Damage;
        }

        public void Attack(IPlayer defender)
        {
            defender.Health -= AttackStrength;
        }

        public void EnemyAttack(IPlayer attacker)
        {
            attacker.Health -= attackStrength;
        }

        public string ShowInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"#### {name} ####\n");
            sb.Append($"Health: {Health}\n");
            sb.Append($"Attack Strength: {AttackStrength}\n");
            sb.Append($"Defense Strength : {DefenseStrength}\n");
            if (IsAlive) {
                sb.Append($"Status: Alive");
            }
            else
            {
                sb.Append($"Status: Dead");
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            if (IsAlive)
            {
                return $"{Name} - {Health} ({GetType().Name.Substring(0, 1).ToUpper()})";
            }
            else
            {
                return $"{Name} - {Health} ({GetType().Name.Substring(0, 1).ToUpper()}) [DEAD]";
            }
        }
    }
}