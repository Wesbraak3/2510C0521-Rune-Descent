using System.Collections;
using UnityEngine;

namespace PlayerController {
    public class PlayerController : MonoBehaviour {
        private CharacterController _characterController;

        public float movementSpeed = 10f, rotationSpeed = 5f;
        private float _rotationY;

        // Use this for initialization
        void Start() {
            _characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector2 movementVector) {
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