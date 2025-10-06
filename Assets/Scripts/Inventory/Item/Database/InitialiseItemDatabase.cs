using UnityEngine;

namespace ItemManagementSystem {
    public class InitialiseItemDatabase : MonoBehaviour {
        public void Awake() {
            ItemDatabase.Initialize();
        }
        private void PrintAllItems() {
            Debug.Log("=== All Items in Database ===");
            foreach (var kvp in ItemDatabase.GetAllItems()) {
                ItemData item = kvp.Value;
                Debug.Log($"Name: {item.itemName}, Category: {item.category}, Description: {item.itemDescription}");
            }
        }
    }
}