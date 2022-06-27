using System.Linq;
using GameEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionRebindController : MonoBehaviour
    {
        [SerializeField] private ControlSchemeSwitchedGameEvent gameEvent;
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputAction excludeBindings;
        [SerializeField] private InputAction cancelBindings;

        public UnityEvent onRebindStarted;
        public UnityEvent onRebindCompleted;
        public UnityEvent onRebindCancelled;

        private InputControlScheme _currentScheme;

        private void OnEnable()
        {
            gameEvent.AddListener(OnControlSchemeSwitched);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(OnControlSchemeSwitched);
        }

        private void OnControlSchemeSwitched(ControlSchemeSwitchedGameEventData data)
        {
            _currentScheme = data.ControlScheme;
        }

        public void StartRebind()
        {
            var inputAction = inputActionReference.action;
            var bindingIndex = inputAction.GetBindingIndex(_currentScheme.bindingGroup);

            inputAction.Disable();

            var rebindingOperation = inputAction.PerformInteractiveRebinding(bindingIndex)
                .WithBindingGroup(_currentScheme.bindingGroup).OnMatchWaitForAnother(0.1f).OnComplete(operation =>
                {
                    inputAction.Enable();
                    operation.Dispose();
                    onRebindCompleted?.Invoke();
                }).OnCancel(operation =>
                {
                    inputAction.Enable();
                    operation.Dispose();
                    onRebindCancelled?.Invoke();
                });

            rebindingOperation = excludeBindings.bindings.Aggregate(rebindingOperation,
                (current, binding) => current.WithControlsExcluding(binding.path));
            rebindingOperation = inputActionReference.asset.bindings.Aggregate(rebindingOperation,
                (current, binding) => current.WithControlsExcluding(binding.path));
            rebindingOperation = cancelBindings.bindings.Aggregate(rebindingOperation,
                (current, binding) => current.WithCancelingThrough(binding.path));

            rebindingOperation.Start();
            onRebindStarted?.Invoke();
        }

        public void ResetRebinds()
        {
            inputActionReference.action.RemoveAllBindingOverrides();
        }
    }
}