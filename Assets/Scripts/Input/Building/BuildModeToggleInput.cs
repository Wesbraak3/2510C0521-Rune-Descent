using UnityEngine;
using UnityEngine.InputSystem;

public class IsBuildModeModeActiveEvent {
    public bool IsBuildModeModeActive;
    public IsBuildModeModeActiveEvent(bool isBuildModeModeActive) => IsBuildModeModeActive = isBuildModeModeActive;
}

public class BuildModeToggleInput : MonoBehaviour {
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private bool disableMovement = false;
    private bool buildMode = false;

    private void OnEnable() {
        inputAction.action.Enable();
        inputAction.action.started += OnActionStarted;
    }
    private void OnDisable() {
        inputAction.action.started -= OnActionStarted;
        inputAction.action.Disable();
    }

    private void OnActionStarted(InputAction.CallbackContext ctx) {
        buildMode = !buildMode;

        if (disableMovement) {
            EventBus.Publish(new IsMoventEnabled(buildMode));
        }

        EventBus.Publish(new IsBuildModeModeActiveEvent(buildMode));
    }
}