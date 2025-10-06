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
        [Header("Core Info")]
        public string itemName;
        [TextArea] public string itemDescription;
        public int value;
        public int maxStack;
        public bool randomise;

        public Sprite itemIcon;
        public GameObject itemModel;

        [Header("Category")]
        public ItemCategory category;

        [Header("Subtype")]
        public WeaponType weaponType;
        public ArmorType armorType;

        [Header("Item Stats")]
        public List<Stat> stats = new();
    }
}