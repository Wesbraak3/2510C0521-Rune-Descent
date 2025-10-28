using System.Collections.Generic;
using UnityEngine;


namespace GridSystem {
    [RequireComponent(typeof(Grid))]
    public class GridManager : MonoBehaviour {
        private static GridManager _instance;

        public static GridManager Instance {
            get {
                if (_instance == null)
                    _instance = FindFirstObjectByType<GridManager>();
                return _instance;
            }
        }

        [Header("References")]
        [SerializeField] private Grid grid;

        [Header("Settings")]
        [SerializeField] private float tileSize = 1f;

        private readonly HashSet<Vector3Int> occupiedPositions = new();

        private void Awake() {
            // Allow manual override
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
                Debug.LogWarning("Multiple GridManagers detected. Using the first one found.");

            if (grid == null)
                grid = GetComponent<Grid>();

            // Swizzle for 3D alignment
            grid.cellSwizzle = GridLayout.CellSwizzle.XZY;
        }

        #region Grid Conversion

        public Vector3Int WorldToGrid(Vector3 worldPosition) {
            return grid.WorldToCell(worldPosition);
        }

        public Vector3 GridToWorld(Vector3Int gridPosition) {
            return grid.GetCellCenterWorld(gridPosition);
        }

        #endregion

        #region Walkability & Occupancy

        public bool IsOccupied(Vector3Int cell) {
            return occupiedPositions.Contains(cell);
        }

        public void SetOccupied(Vector3Int cell, bool occupied) {
            if (occupied)
                occupiedPositions.Add(cell);
            else
                occupiedPositions.Remove(cell);
        }

        public bool IsWalkable(Vector3Int cell) {
            return !occupiedPositions.Contains(cell);
        }

        #endregion

        #region Debug

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            foreach (var pos in occupiedPositions) {
                Gizmos.DrawWireCube(GridToWorld(pos), Vector3.one * (tileSize * 0.9f));
            }
        }

        #endregion
    }
}