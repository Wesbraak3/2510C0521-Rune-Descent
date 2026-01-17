using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : BaseInput {
    private InputSystem_Actions actions;

    private Vector2 move;
    private Vector2 mousePos;
    private bool rightClickHeld;

    public override Vector2 MoveDirection => move;
    public override Vector2 MousePosition => mousePos;
    public override bool IsLooking => rightClickHeld;

    protected void Awake() {
        actions = new InputSystem_Actions();
    }

    private void OnEnable() {
        //if (!isOwner) return;
        actions.Enable();

        actions.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        actions.Player.Move.canceled += _ => move = Vector2.zero;

        actions.Player.MousePosition.performed += ctx => mousePos = ctx.ReadValue<Vector2>();

        actions.Player.RightClick.performed += _ => rightClickHeld = true;
        actions.Player.RightClick.canceled += _ => rightClickHeld = false;
    }

    private void OnDisable() {
        //if (!isOwner) return;
        actions.Disable();
    }
}
