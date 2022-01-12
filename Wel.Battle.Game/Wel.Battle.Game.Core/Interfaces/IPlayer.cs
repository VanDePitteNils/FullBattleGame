using System.Collections.Generic;
using Wel.Battle.Game.Core.Entities;
using Wel.Battle.Game.Core.Entities.Weapons;

namespace Wel.Battle.Game.Core.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        int Health { get; set; }
        List<IInventoryItem> Inventory { get; }
        int AttackStrength { get; set; }
        int DefenseStrength { get; set; }

        void Equip(Weapon item);
        void Attack(IPlayer defender);
    }
}
