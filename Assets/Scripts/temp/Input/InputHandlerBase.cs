using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandler {
    public abstract class InputHandlerBase : MonoBehaviour {
        [SerializeField] protected InputActionReference inputAction;
        protected InputAction Action => inputAction?.action;

        protected virtual void OnEnable() {
            if (inputAction == null || inputAction.action == null) {
                Debug.LogError($"{GetType().Name}: InputActionReference not assigned or missing an action!");
                return;
            }

            Action.Enable();
            Subscribe();
        }

        protected virtual void OnDisable() {
            if (Action == null) return;
            Unsubscribe();
            Action.Disable();
        }

        protected virtual void Subscribe() { }

        protected virtual void Unsubscribe() { }
    }
}