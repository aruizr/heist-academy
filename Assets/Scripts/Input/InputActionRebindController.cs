using System.Linq;
using Codetox.Variables;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionRebindController : SettingController
    {
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private Variable<InputControlScheme> currentControlScheme;
        [SerializeField] private InputAction excludeBindings;
        [SerializeField] private InputAction cancelBindings;

        public UnityEvent onRebindStarted;
        public UnityEvent onRebindCompleted;
        public UnityEvent onRebindCancelled;

        public void StartRebind()
        {
            var inputAction = inputActionReference.action;
            var bindingGroup = currentControlScheme.Value.bindingGroup;
            var bindingIndex = inputAction.GetBindingIndex(bindingGroup);

            inputAction.Disable();

            var rebindingOperation = inputAction.PerformInteractiveRebinding(bindingIndex)
                .WithBindingGroup(bindingGroup).OnMatchWaitForAnother(0.1f).OnComplete(operation =>
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

        public override void ResetValue()
        {
            inputActionReference.action.RemoveAllBindingOverrides();
        }
    }
}