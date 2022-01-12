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

        public void InitLists()
        {
            players = new List<Player>();
            attackers = new List<Player>();
            defenders = new List<Player>();
        }
    }
}
