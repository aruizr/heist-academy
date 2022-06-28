using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    public class BasicInputActionHandler : MonoBehaviour
    {
        [SerializeField] private InputAction action;

        public UnityEvent onStarted, onPerformed, onCancelled;

        private void Start()
        {
            action.started += context => onStarted?.Invoke();
            action.performed += context => onPerformed?.Invoke();
            action.canceled += context => onCancelled?.Invoke();
        }

        private void OnEnable()
        {
            action.Enable();
        }

        private void OnDisable()
        {
            action.Disable();
        }
    }
}