using UnityEngine;

public class PlayerClicker : MonoBehaviour {
    private InputSystem_Actions inputActions;

    private void Awake() {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    private void Update() {
        // Check if mouse button was clicked
        if (inputActions.Player.MouseLeft.WasPerformedThisFrame()) {
            // Get the current mouse position from the input system
            Vector2 mousePosition = inputActions.Player.MousePosition.ReadValue<Vector2>();

            // Create a ray from camera through mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Raycast into the world
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                var clickedObject = hit.collider.gameObject;

                // If object has EnemyHealth, apply damage
                if (clickedObject.TryGetComponent(out IDamageable damageable)) {
                    damageable.OnHit(10, this);
                }
            }
        }

        if (inputActions.Player.MouseRight.WasPerformedThisFrame()) {
            // Get the current mouse position from the input system
            Vector2 mousePosition = inputActions.Player.MousePosition.ReadValue<Vector2>();

            // Create a ray from camera through mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Raycast into the world
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                var clickedObject = hit.collider.gameObject;

                // If object has EnemyHealth, apply damage
                if (clickedObject.TryGetComponent(out IHealable healable)) {
                    healable.OnHeal(10);
                }
            }
        }
    }
}
