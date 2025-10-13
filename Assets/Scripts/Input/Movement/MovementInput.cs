using UnityEngine;

namespace InputHandler {
    public class MovementInput : InputHandlerBase {
        [SerializeField] private bool inputEnabled = true;

        protected override void OnEnable() {
            base.OnEnable();
            EventBus.Subscribe<EnableMovementEvent>(SetInputEnabled);
        }

        protected override void OnDisable() {
            base.OnDisable();
            EventBus.Unsubscribe<EnableMovementEvent>(SetInputEnabled);
        }

        private void Update() {
            if (!inputEnabled) return;

            Vector2 move = Action.ReadValue<Vector2>();
            EventBus.Publish(new MeveInputEvent(move));
        }
        private void SetInputEnabled(EnableMovementEvent e) => inputEnabled = e.InputEnabled;
    }
}