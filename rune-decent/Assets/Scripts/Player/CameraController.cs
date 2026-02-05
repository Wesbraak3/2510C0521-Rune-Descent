using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private CinemachineCamera _playerCamera;

    void Awake() {
        _playerCamera.transform.SetParent(null);
    }
}