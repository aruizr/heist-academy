using UnityEngine.Events;

namespace Interactions.Final
{
    public class Switch : Selectible, IInteractable, ISwitch
    {
        public UnityEvent onActivated;
        public UnityEvent onDeactivated;

        private bool _isActivated;

        public void Interact()
        {
            Toggle();
        }

        public void Toggle()
        {
            _isActivated = !_isActivated;
            (_isActivated ? onActivated : onDeactivated)?.Invoke();
        }
    }
}