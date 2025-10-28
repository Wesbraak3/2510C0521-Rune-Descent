using GridSystem;
using InputHandler;
using System.Collections;
using UnityEngine;

namespace PlayerController {
    public class GridMovement : InputHandlerBase {
        [Header("Movement Settings")]
        [SerializeField] private float moveTime = 0.2f;

        private Vector3Int gridPosition;
        private bool isMoving = false;
        private Vector3 targetPos;

        private GridSystem.GridManager gridManager;

        private void Awake() {
            gridManager = GridManager.Instance;

            if (gridManager == null) {
                Debug.LogError("PlayerController: GridManager not found. Make sure it’s registered in GameRoot.");
            }
        }

        private void Start() {
            Vector3Int cell = gridManager.WorldToGrid(transform.position);
            gridPosition = cell;
            targetPos = gridManager.GridToWorld(cell);

            gridManager.SetOccupied(cell, true);
        }

        private void Update() {
            Move(Action.ReadValue<Vector2>());
        }

        private void Move(Vector2 dir) {
            if (isMoving) return;

            // Set direction to move to
            Vector2Int direction = new(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y));
            if (direction == Vector2Int.zero) return;

            // Get Cell to move to
            Vector3Int currentCell = new(gridPosition.x, gridPosition.y, 0);
            Vector3Int targetCell = currentCell + new Vector3Int(direction.x, direction.y, 0);

            // Check if target cell is walkable
            if (!gridManager.IsWalkable(targetCell))
                return;

            // Move to cell pass turn
            StartCoroutine(MoveFromTo(currentCell, targetCell));
        }

        private IEnumerator MoveFromTo(Vector3Int currentCell, Vector3Int targetCell) {
            isMoving = true;

            // Update occupancy
            gridManager.SetOccupied(currentCell, false);
            gridManager.SetOccupied(targetCell, true);

            // Calculate target position in world space
            Vector3 targetWorld = gridManager.GridToWorld(targetCell);

            // Smooth move towards the target
            while ((transform.position - targetWorld).sqrMagnitude > Mathf.Epsilon) {
                transform.position = Vector3.MoveTowards(transform.position, targetWorld, Time.deltaTime / moveTime);
                yield return null;
            }

            // Snap to exact grid center
            transform.position = targetWorld;
            gridPosition = targetCell;
            isMoving = false;
        }
    }
}