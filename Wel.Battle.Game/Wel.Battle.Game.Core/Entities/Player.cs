﻿using System;
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
        public int AttackStrength { get; set; }
        public int DefenseStrength { get; set; }

        readonly Random rnd = new Random();

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
                    return 0;
                }
                return health;
            }
            set
            {
                if(health <= 0)
                {
                    IsAlive = false;
                }
                health = value;
            }
        }

        public void Equip (Weapon weapon)
        {
            AttackStrength += weapon.Damage;
        }

        public void Attack(IPlayer defender)
        {
            defender.Health -= AttackStrength;
        }

        public string ShowInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"#### {name} ####\n");
            sb.Append($"Health: {Health}\n");
            sb.Append($"Attack Strength: {AttackStrength}\n");
            sb.Append($"Defense Strength : {DefenseStrength}");
            return sb.ToString();
        }

        public Player(string name)
        {
            Name = name ;
            Health = 100;
            IsAlive = true;
            HasWeapon = false;
            AttackStrength = rnd.Next(5,15);
            DefenseStrength = 10;
        }

        public override string ToString()
        {
            if (IsAlive)
            {
                return $"{Name} - {Health} ({GetType().Name.Substring(0,1).ToUpper()})";
            }
            else if (HasWeapon)
            {
                return $"{Name} - {Health} ({GetType().Name.Substring(0, 1).ToUpper()}) (W)";
            }
            else
            {
                return $"{Name} - {Health} ({GetType().Name.Substring(0, 1).ToUpper()}) [DEAD]";
            }
        }
    }
}