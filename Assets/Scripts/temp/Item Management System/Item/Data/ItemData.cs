using System.Collections.Generic;
using UnityEngine;

namespace ItemManagementSystem {
    public enum ItemCategory {
        Weapon,
        Armor,
        Consumable,
        Material,
        Quest,
        Misc
    }

    public enum WeaponType {
        Sword,
        Bow,
        Axe,
        Staff
    }

    public enum ArmorType {
        Helmet,
        Chest,
        Gauntlet,
        Greaves
    }

    [CreateAssetMenu(menuName = "Inventory/Item Data")]
    public class ItemData : ScriptableObject {
        public string itemName;
        [TextArea] public string itemDescription;
        public int value;
        public int maxStack;
        public bool randomizeStats;

        public Sprite itemIcon;
        public GameObject itemModel;

        public ItemCategory category;

        public WeaponType weaponType;
        public ArmorType armorType;

        public List<Stat> stats = new();
    }
}