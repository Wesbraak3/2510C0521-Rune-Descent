using System.Collections;
using UnityEngine;

namespace PlayerController {
    public class PlayerController : MonoBehaviour {
        private CharacterController _characterController;

        public float moveTime = 0.2f;
        public Vector2Int gridPosition;
        private bool isMoving = false;
        private Vector3 targetPos;

        private void OnEnable() {
            EventBus.Subscribe<MeveInputEvent>(Move);
        }

        private void OnDisable() {
            EventBus.Unsubscribe<MeveInputEvent>(Move);

        }

        void Start() {
            targetPos = transform.position;
        }

        void Update() {
            if (isMoving)
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime / moveTime);
        }

        public void Move(MeveInputEvent e) {
            if (isMoving) return;

            Vector2 input = e.MoveVector;
            Vector2Int direction = new (Mathf.RoundToInt(input.x), Mathf.RoundToInt(input.y));

            if (direction != Vector2Int.zero)
                StartCoroutine(MoveStep(direction));
        }

        private IEnumerator MoveStep(Vector2Int dir) {
            isMoving = true;
            gridPosition += dir;
            targetPos = new Vector3(gridPosition.x, gridPosition.y, 0f);

            while ((transform.position - targetPos).sqrMagnitude > Mathf.Epsilon)
                yield return null;

            transform.position = targetPos;
            isMoving = false;
        }
    }
}