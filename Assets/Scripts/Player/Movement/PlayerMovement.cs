using PurrNet;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkIdentity {

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;

    [SerializeField] private float _speed;
    [SerializeField] private CinemachineCamera _playerCamera;

    private Rigidbody _rigidbody;

    private void Awake() {
        TryGetComponent(out _rigidbody);
        _inputActions = new InputSystem_Actions();
    }

    void OnEnable() {
        _inputActions.Enable();
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += _ => _moveInput = Vector2.zero;
    }

    void OnDisable() {
        _inputActions.Disable();
    }

    protected override void OnSpawned() {
        base.OnSpawned();

        enabled = isOwner;

        _playerCamera.Priority.Value = isOwner ? 10 : 0;
        if (isOwner) {
            _playerCamera.transform.SetParent(null);
        }
    }

    private void FixedUpdate() {
        Vector3 input = new(_moveInput.x, 0, _moveInput.y);
        input.Normalize();

        var movement = input * _speed;
        _rigidbody.linearVelocity = movement;

        if (_rigidbody.linearVelocity != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity, Vector3.up);
        }
    }
}
