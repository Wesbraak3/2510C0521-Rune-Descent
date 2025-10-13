using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Grid grid; 
    
    [Header("Settings")]
    [SerializeField] private float tileSize = 1f;

    private HashSet<Vector3Int> occupiedPositions = new();

    private void Awake() {
        if (grid == null) {
            grid = GetComponent<Grid>();
        }
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
