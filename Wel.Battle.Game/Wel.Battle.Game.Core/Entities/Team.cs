using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wel.Battle.Game.Core.Entities
{
   public class Team
    {
        public List<Player> Players { get; set; }
        public int Balance { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Gelieve een team naam op te geven");
                } else if (value.Length > 20)
                {
                    name = value.Trim().Substring(0,20);
                }
                name = value.Trim(); 
            }
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }


        public Team(string name)
        {
            Name = name;
        }
    }
}
