using System;
using System.Collections.Generic;

namespace ItemManagementSystem {

    [Serializable]
    public class Item {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Value { get; private set; }
        public Dictionary<StatType, float> Stats { get; private set; }

        public ItemCategory Category { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public ArmorType ArmorType { get; private set; }

        // contains magic numbers !!!! CHANGE 
        public int StackSize { get; set; } = 1;
        public int MaxStack { get; private set; }

        public Item(ItemData baseData, int StackSize = 1, bool randomize = false) {
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
                if (randomize) statValue *= UnityEngine.Random.Range(0.8f, 1.2f);
                Stats[stat.type] = statValue;
            }
        }
        public Item(string itemName, int StackSize = 1, bool randomize = false) 
            : this(ItemDatabase.GetItemDataByName(itemName), StackSize, randomize) { }

        public bool HasStat(StatType type) => Stats.ContainsKey(type);
        public float GetStat(StatType type) => Stats.TryGetValue(type, out var value) ? value : 0f;
        
        // meta data in item are changeble 
        public float ModifyStat(StatType type, float amount) {
            if (type.GetCategory() != StatCategory.Meta && 
                !Stats.ContainsKey(type)) return -1f;
            Stats[type] += amount;
            return Stats[type];
        }
    }
}
