using PurrNet;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : NetworkIdentity {

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;

    [SerializeField] private float _speed;
    [SerializeField] private CinemachineCamera _playerCamera;

    private Rigidbody _rigidbody;

    [SerializeField] private LayerMask _groundMask;
    private bool _isRightClickHeld;

    private void Awake() {
        TryGetComponent(out _rigidbody);
        _inputActions = new InputSystem_Actions();
    }

    void OnEnable() {
        _inputActions.Enable();
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += _ => _moveInput = Vector2.zero;

        _inputActions.Player.RightClick.performed += _ => _isRightClickHeld = true;
        _inputActions.Player.RightClick.canceled += _ => _isRightClickHeld = false;
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


        if (_isRightClickHeld) {
            RotateTowardMouse();
        }
        else if (_rigidbody.linearVelocity.sqrMagnitude > 0.01f) {
            transform.rotation = Quaternion.LookRotation(_rigidbody.linearVelocity, Vector3.up);
        }
    }

    private void RotateTowardMouse() {
        Vector2 mousePosition = _inputActions.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _groundMask)) {
            Vector3 lookPoint = hit.point;
            Vector3 direction = lookPoint - transform.position;
            direction.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 15f);
        }
    }
}
