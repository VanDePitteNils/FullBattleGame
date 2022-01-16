using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wel.Battle.Game.Core.Entities;
using Wel.Battle.Game.Core.Entities.Classes;
using Wel.Battle.Game.Core.Entities.Weapons;

namespace Wel.Battle.Game.Core.Services
{
    public class BattleGameService
    {
        public List<Player> players;
        public List<Player> attackers;
        public List<Player> defenders;
        public readonly List<string> names = new() {"Alice", "Bob", "Carol", "Dave", "Eve", "Freddy", "Garry", "Harold", "Ian"};

        public BattleGameService()
        {
            InitLists();
        }

        public string ShowPlayerInfo(Player player)
        {
            if (player != null)
            {
                return player.ShowInfo();
            }
            return null;
        }

        public void AddDefender(Player player)
        {
            defenders.Add(player);
        }

        public void AddAttacker(Player player)
        {
            attackers.Add(player);
        }

        public void AttackEnemy(Player attacker, Player defender)
        {
            if(attacker != null)
            {
                attacker.Attack(defender);
            }
        }

        public void EnemyAttack(Player defender, Player attacker)
        {
            if (defender != null)
            {
                defender.Attack(attacker);
            }
        }

        public string FriendlyAttackBattleChat(Player attacker, Player defender)
        {
            if(attacker != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{attacker.GetType().Name} {attacker.Name} has dealth {attacker.AttackStrength} to {defender.GetType().Name} {defender.Name}\n");
                return sb.ToString();
            }
            return null;
        }

        public string FriendlyAbilityBattleChat(Player attacker, Player defender)
        {
            string attackertype = attacker.GetType().Name;
            string defenderType = defender.GetType().Name;

            StringBuilder sb = new StringBuilder();
            if(attacker is Mage)
            {
                sb.Append($"{attackertype} {attacker.Name} used Fireball dealing 20 area damage\n");
            }else if(attacker is Assassin)
            {
                sb.Append($"{attackertype} {attacker.Name} used leach dealing 10 damage to {defenderType} {defender.Name}\n while {attackertype} {attacker.Name} gains 10hp\n");
            }else if(attacker is Tank)
            {
                sb.Append($"{attackertype} {attacker.Name} has gone beserk increasing his power but lowering his defense\n while dealing 20 damage to {defenderType} {defender.Name}\n");
            }
            return sb.ToString();
        }

        public string EnemyAttackBattleChat(Player defender, Player attacker)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{defender.GetType().Name} {defender.Name} has dealth {defender.AttackStrength} to {attacker.GetType().Name} {attacker.Name}\n");
            return sb.ToString();
        }

        public void AddMage(string name)
        {
            players.Add(new Mage(name));
        }

        public void AddTank(string name)
        {
            players.Add(new Tank(name));
        }

        public void AddAssassin(string name)
        {
            players.Add(new Assassin(name));
        }

        private void InitLists()
        {
            players = new List<Player>();
            attackers = new List<Player>();
            defenders = new List<Player>();
        }
    }
}
