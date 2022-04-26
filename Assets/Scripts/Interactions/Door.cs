using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private UnityEvent onOpened;
        [SerializeField] private UnityEvent onClosed;

        public bool IsOpen { get; private set; }

        public void Open()
        {
            IsOpen = true;
            onOpened?.Invoke();
        }

        public void Close()
        {
            IsOpen = false;
            onClosed?.Invoke();
        }

        public void ToggleOpenClose()
        {
            if (IsOpen) Close();
            else Open();
        }
    }
}