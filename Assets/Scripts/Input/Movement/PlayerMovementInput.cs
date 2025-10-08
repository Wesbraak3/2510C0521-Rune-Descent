using UnityEngine;
using UnityEngine.InputSystem;

public class MovementEvent {
    public Vector2 MoveVector;
    public MovementEvent(Vector2 moveVector) => MoveVector = moveVector;
}

public class IsMoventEnabled {
    public bool InputEnabled;
    public IsMoventEnabled(bool inputEnabled) => InputEnabled = inputEnabled;
}

public class PlayerMovementInput : MonoBehaviour {
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private bool inputEnabled = true;

    private void OnEnable() {
        if (inputAction == null) {
            Debug.LogError($"{nameof(PlayerMovementInput)}: InputActionReference not assigned!");
            return;
        }

        inputAction.action.Enable();
        EventBus.Subscribe< IsMoventEnabled>(SetInputEnabled);
    }

    private void OnDisable() {
        inputAction.action.Disable();
        EventBus.Unsubscribe<IsMoventEnabled>(SetInputEnabled);
    }

    private void Update() {
        if (!inputEnabled) return;
        Vector2 move = inputAction.action.ReadValue<Vector2>();
        EventBus.Publish(new MovementEvent(move));
    }

    public void SetInputEnabled(IsMoventEnabled e) => inputEnabled = e.InputEnabled;
}