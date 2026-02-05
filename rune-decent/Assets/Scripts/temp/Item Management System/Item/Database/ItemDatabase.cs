using ItemManagementSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ItemManagementSystem {
    internal static class ItemDatabase {
        private static Dictionary<string, ItemData> itemsByName;
        private static bool initialized = false;

        internal struct AccessKey { }

        private static void Initialize() {
            itemsByName = new Dictionary<string, ItemData>();
            var allItems = Resources.LoadAll<ItemData>("Items");

            foreach (var item in allItems) {
                if (!itemsByName.ContainsKey(item.itemName))
                    itemsByName.Add(item.itemName, item);
            }

            initialized = true;
        }

        internal static ItemData GetItemDataByName(string name, AccessKey _) {
            if (!initialized) Initialize();
            itemsByName.TryGetValue(name, out var data);
            return data;
        }
    }
}