using System.Linq;
using MyBox;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Interactions
{
    public class Door : MonoBehaviour, IDoor, ISwitch
    {
        [SerializeField] private PropertyTweener<Transform, Vector3> tweener;
        [SerializeField] private bool isLocked;

        [SerializeField] [ConditionalField(nameof(isLocked))]
        private GameObject unlockedBy;

        [SerializeField] [ConditionalField(nameof(isLocked))]
        private GameObjectRuntimeSet inventory;
        
        public UnityEvent onStartedOpening;
        public UnityEvent onFinishedOpening;
        public UnityEvent onStartedClosing;
        public UnityEvent onFinishedClosing;

        [ConditionalField(nameof(isLocked))] public UnityEvent onLocked;

        private void OnEnable()
        {
            tweener.onComplete.AddListener(OnComplete);
        }

        private void OnDisable()
        {
            tweener.onComplete.RemoveListener(OnComplete);
        }

        public bool IsOpen { get; private set; }

        public void Open()
        {
            if (isLocked && !inventory.Contains(unlockedBy))
            {
                onLocked?.Invoke();
                return;
            }

            ForceOpen();
        }

        public void ForceOpen()
        {
            IsOpen = true;
            onStartedOpening?.Invoke();
        }

        public void Close()
        {
            IsOpen = false;
            onStartedClosing?.Invoke();
        }

        public void Toggle()
        {
            if (IsOpen) Close();
            else Open();
        }

        private void OnComplete()
        {
            (IsOpen ? onFinishedOpening : onFinishedClosing)?.Invoke();
        }
    }
}