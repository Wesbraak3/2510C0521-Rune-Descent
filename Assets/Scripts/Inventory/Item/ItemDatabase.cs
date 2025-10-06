using System.Collections.Generic;
using UnityEngine;

namespace ItemManagementSystem {
    public static class ItemDatabase {
        private static Dictionary<string, ItemData> itemsByName;
        private static bool init = false;

        public static void Initialize() {
            itemsByName = new Dictionary<string, ItemData>();

            // Corrected path
            var allItems = Resources.LoadAll<ItemData>("Items");

            foreach (var item in allItems) {
                if (!itemsByName.ContainsKey(item.itemName))
                    itemsByName.Add(item.itemName, item);
            }

            init = true;
            Debug.Log($"ItemDatabase initialized with {itemsByName.Count} items.");
        }

        public static Dictionary<string, ItemData> GetAllItems() {
            if (!init) Initialize();
            return itemsByName;
        }

        public static ItemData GetItemDataByName(string name) {
            if (!init) Initialize();
            if (itemsByName.TryGetValue(name, out var data))
                return data;

            Debug.LogWarning("ItemData not found: " + name);
            return null;
        }
    }
}
