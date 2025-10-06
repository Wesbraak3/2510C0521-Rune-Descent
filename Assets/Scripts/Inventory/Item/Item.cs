using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagementSystem {

    [Serializable]
    public class Item {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }

        public ItemCategory Category { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public ArmorType ArmorType { get; private set; }

        public int StackSize { get; set; } = 1;
        public int MaxStack { get; private set; }
        public int Durability { get; private set; }
        public int Uses { get; private set; }

        public Dictionary<StatType, float> Stats { get; private set; }
        public Item(ItemData baseData, int stackSize = 1) {
            Id = Guid.NewGuid().ToString();
            Name = baseData.itemName;
            Description = baseData.itemDescription;
            Value = baseData.value;
            Category = baseData.category;
            WeaponType = baseData.weaponType;
            ArmorType = baseData.armorType;
            MaxStack = baseData.maxStack;

            Stats = new Dictionary<StatType, float>();
            foreach (var stat in baseData.stats) {
                float statValue = stat.value;

                if (baseData.randomize)
                    statValue *= UnityEngine.Random.Range(0.8f, 1.2f);

                Stats[stat.type] = statValue;

                // initialize meta values if defined
                switch (stat.type) {
                    case StatType.MaxDurability:
                        Durability = Mathf.RoundToInt(statValue);
                        break;

                    case StatType.MaxUses:
                        Uses = Mathf.RoundToInt(statValue);
                        break;
                }
            }

            StackSize = Mathf.Clamp(stackSize, 1, MaxStack);
        }
        public Item(string itemName, int stackSize = 1) 
            : this(ItemDatabase.GetItemDataByName(itemName), stackSize) { }

        public bool HasStat(StatType type) => Stats.ContainsKey(type);
        public float GetStat(StatType type) => Stats.TryGetValue(type, out var value) ? value : 0f;

        public bool TryUse() {
            if (Uses <= 0) return false;
            Uses--;
            return Uses <= 0;
        }

        public bool TryApplyDurabilityLoss(int amount = 1) {
            if (Durability <= 0) return false;
            Durability -= amount;
            return Durability <= 0;
        }
    }
}
