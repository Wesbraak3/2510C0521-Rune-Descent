using UnityEngine;
using UnityEngine.InputSystem;


namespace InputHandler {
    public class BuildModeToggleInput : InputHandlerBase {
        [SerializeField] private bool disableMovement = true;
        private bool buildMode = false;

        protected override void Subscribe() {
            if (Action != null)
                Action.started += ToggleBuildMode;
        }

        protected override void Unsubscribe() {
            if (Action != null)
                Action.started -= ToggleBuildMode;
        }

        private void ToggleBuildMode(InputAction.CallbackContext ctx) {
            buildMode = !buildMode;

            // Broadcast build mode state
            EventBus.Publish(new IsBuildModeModeActiveEvent(buildMode));

            // Handle movement enable/disable logic
            bool allowMovement = !(disableMovement && buildMode);
            EventBus.Publish(new EnableMovementEvent(allowMovement));

            Debug.Log($"[Input] Build Mode: {buildMode}, Movement Enabled: {allowMovement}");
        }
    }
}