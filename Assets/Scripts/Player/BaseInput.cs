using PurrNet;
using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour, IMoveInput, ILookDirection {
    public abstract Vector2 MoveDirection { get; }
    public abstract Vector2 MousePosition { get; }
    public abstract bool IsLooking { get; }

    //[SerializeField] private CinemachineCamera _playerCamera;

    //protected override void OnSpawned() {
    //    base.OnSpawned();

    //    enabled = isOwner;

    //    _playerCamera.Priority.Value = isOwner ? 10 : 0;
    //    if (isOwner) {
    //        _playerCamera.transform.SetParent(null);
    //    }
    //}
}