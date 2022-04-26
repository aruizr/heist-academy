using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private UnityEvent onOpened;
        [SerializeField] private UnityEvent onClosed;

        private bool _isOpen;

        public void Open()
        {
            _isOpen = true;
            onOpened?.Invoke();
        }

        public void Close()
        {
            _isOpen = false;
            onClosed?.Invoke();
        }

        public void ToggleOpenClose()
        {
            if (_isOpen) Close();
            else Open();
        }
    }
}