using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Switch : MonoBehaviour, ISwitch
    {
        public UnityEvent onActivated;
        public UnityEvent onDeactivated;

        private bool _isActivated;

        public void Toggle()
        {
            _isActivated = !_isActivated;
            (_isActivated ? onActivated : onDeactivated)?.Invoke();
        }
    }
}