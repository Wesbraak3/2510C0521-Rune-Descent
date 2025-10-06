using UnityEngine;
using UnityEngine.InputSystem;
using ItemManagementSystem;

public class testinputs: MonoBehaviour {

    public ItemManagementSystem.Inventory inventory;

    private void Update() {
        if (Keyboard.current.gKey.wasPressedThisFrame) {
            inventory.AddItem(new("Tutor Sword", true));
            Debug.Log("G key was pressed!");
        }
        if (Keyboard.current.hKey.wasPressedThisFrame) {
        }
    }
}