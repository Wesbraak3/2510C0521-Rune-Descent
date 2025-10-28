using UnityEngine;
using UnityEngine.InputSystem;

namespace player {
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour {
        private CharacterController controller;
        private InputSystem_Actions inputActions;
        private Vector2 moveInput;

        [Header("Movement Settings")]
        public float moveSpeed = 5f;
        public float gravity = -9.81f;
        private Vector3 velocity;

        void Awake() {
            controller = GetComponent<CharacterController>();
            inputActions = new InputSystem_Actions();
        }

        void OnEnable() {
            inputActions.Enable();
            inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += _ => moveInput = Vector2.zero;
        }

        void OnDisable() => inputActions.Disable();

        void Update() {
            // Apply gravity
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -2f;
            velocity.y += gravity * Time.deltaTime;

            // Convert move input to world space
            Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

            // Normalize diagonal movement
            if (move.magnitude > 1f)
                move.Normalize();

            // Apply movement
            controller.Move(move * moveSpeed * Time.deltaTime + velocity * Time.deltaTime);
        }
    }

}
