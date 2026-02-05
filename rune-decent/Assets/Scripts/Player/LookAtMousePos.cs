using UnityEngine;

public class LookAtMousePos : MonoBehaviour {
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float rotationSpeed = 15f;

    private ILookDirection lookAtInput;

    private void Awake() {
        lookAtInput = GetComponent<ILookDirection>();
    }

    private void FixedUpdate() {
        if (lookAtInput == null || !lookAtInput.IsLooking)
            return;

        Ray ray = Camera.main.ScreenPointToRay(lookAtInput.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask)) {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f) return;

            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                target,
                Time.fixedDeltaTime * rotationSpeed
            );
        }
    }
}
