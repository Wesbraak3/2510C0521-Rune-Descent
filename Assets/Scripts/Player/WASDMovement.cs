using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WASDMovement : MonoBehaviour {
    [SerializeField] private float speed = 6f;

    private Rigidbody rb;
    private IMoveInput moveInput;
    private ILookDirection lookDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        var input = GetComponent<BaseInput>();
        moveInput = input;
        lookDirection = input;
    }

    private void FixedUpdate() {
        if (moveInput.MoveDirection == Vector2.zero) 
            return;

        Vector3 move = new (moveInput.MoveDirection.x, 0f, moveInput.MoveDirection.y);

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        rb.linearVelocity = move * speed;

        if (lookDirection != null && !lookDirection.IsLooking && rb.linearVelocity.sqrMagnitude > 0.01f) {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity, Vector3.up);
        }
    }
}
