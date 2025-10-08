using System.Collections;
using UnityEngine;

namespace PlayerController {
    public class PlayerController : MonoBehaviour {
        private CharacterController _characterController;

        public float movementSpeed = 10f, rotationSpeed = 5f;
        private float _rotationY;

        private void OnEnable() {
            EventBus.Subscribe<MovementEvent>(Move);
        }

        private void OnDisable() {
            EventBus.Unsubscribe<MovementEvent>(Move);

        }

        void Start() {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(MovementEvent e) {
            Vector2 movementVector = e.MoveVector;
            Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
            move = movementSpeed * Time.deltaTime * move;
            _characterController.Move(move);
        }

        public void Rotate(Vector2 rotationVector) {
            _rotationY += rotationVector.x * rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
        }
    }
}