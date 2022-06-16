using System;
using System.Linq;
using MyBox;
using PropertyAnimators;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Interactions
{
    public class Door : MonoBehaviour, IDoor, ITogglable
    {
        [SerializeField] private PropertyAnimator<Transform, Vector3> animator;
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
        [ConditionalField(nameof(isLocked))] public UnityEvent onUnlocked;

        private void Start()
        {
            animator.onComplete.AddListener(OnComplete);
        }

        public bool IsOpen { get; private set; }

        public void Open()
        {
            if (!isLocked)
            {
                ForceOpen();
                return;
            }

            if (!inventory.Any())
            {
                onLocked?.Invoke();
                return;
            }

            inventory.Remove(inventory.ElementAt(0));
            isLocked = false;
            onUnlocked?.Invoke();
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