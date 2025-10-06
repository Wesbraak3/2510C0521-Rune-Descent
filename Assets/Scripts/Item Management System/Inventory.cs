using System.Collections.Generic;
using UnityEngine;

namespace ItemManagementSystem {
    public class Inventory : MonoBehaviour {
        [SerializeField]
        private List<Item> items = new();

        [Header("Inventory Settings")]
        [SerializeField]
        private int maxSize = 20;
        public int Size => items.Count;
        public int MaxSize => maxSize;

        public void Start() {

        }

        public bool AddItem(Item item) {
            if (items.Count >= maxSize) {
                Debug.LogWarning("Inventory full! Cannot add item: " + item.Name);
                return false;
            }

            items.Add(item);
            return true;
        }
        public bool RemoveItem(Item item) {
            if (!items.Contains(item))
                return false;

            items.Remove(item);
            return true;
        }

        public List<Item> GetItems() => new(items);
        public Item GetItemById(string id) => items.Find(i => i.Id == id);
    }
}
