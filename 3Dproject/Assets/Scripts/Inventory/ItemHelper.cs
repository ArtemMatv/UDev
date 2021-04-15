using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryNS
{
    public enum ItemId
    {
        Gold = 1,
        Scrap = 2,
        Shard = 3,
        Book = 4,
        SimpleHelmet = 5,
        Cuirass = 6,
        Katana = 7,
        Axe = 8,
    }

    [Serializable]
    public class Stat
    {
        public StatType StatType;
        public int Amount;
    }

    public enum StatType
    {
        Default,
        HP,
        MP,
        Strengh,
        Agility,
        Intelligence,
        Armor,
        Damage,
        AttackSpeed
    }

    public enum EquipmentType
    {
        Weapon = 1,
        Shield,
        Helmet,
        Chest,
        Leggins,
        Boots
    }
}
