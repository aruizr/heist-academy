using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Switch : MonoBehaviour
    {
        [SerializeField] private UnityEvent onActivated;
        [SerializeField] private UnityEvent onDeactivated;

        private bool _isActivated;

        public void Flip()
        {
            _isActivated = !_isActivated;
            (_isActivated ? onActivated : onDeactivated)?.Invoke();
        }
    }
}