using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Door : MonoBehaviour
    {
        public UnityEvent onOpened;
        public UnityEvent onClosed;
        public UnityEvent onFinishOpening;

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

        public void FinishOpening()
        {
            onFinishOpening?.Invoke();
        }
    }
}