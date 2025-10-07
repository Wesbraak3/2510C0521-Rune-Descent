using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayerEvent {
    public Vector2 MovementVector;
    public MovePlayerEvent(Vector2 movementVector) => MovementVector = movementVector;
}

public class BuildModeEvent {
    public bool BuildMode;
    public BuildModeEvent(bool buildMode) => BuildMode = buildMode;
}

namespace PlayerController {
    public class InputHandler : MonoBehaviour {
        public PlayerController charecterController;

        [SerializeField]
        private InputActionReference _moveAction;

        [SerializeField]
        private InputActionReference _BuildMode;
        private bool buildMode = false;

        void Awake() {
            if (_moveAction == null || _BuildMode == null)
                Debug.LogError($"{nameof(InputHandler)}: Input actions not assigned!");
        }

        private void OnEnable() {
            _BuildMode.action.performed += ToggleBuildMode;
        }

        private void OnDisable() {
            _BuildMode.action.performed -= ToggleBuildMode;
        }

        void Start() {
            Cursor.visible = false;
        }

        void Update() {
            EventBus.Publish(new MovePlayerEvent(_moveAction.action.ReadValue<Vector2>()));
        }

        void ToggleBuildMode(InputAction.CallbackContext ctx) {
            buildMode = !buildMode;
            EventBus.Publish(new BuildModeEvent(buildMode));
        }
    }
}