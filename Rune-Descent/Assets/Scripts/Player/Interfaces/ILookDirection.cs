using System.Collections;
using UnityEngine;
public interface ILookDirection {
    Vector2 MousePosition { get; }
    bool IsLooking { get; }
}