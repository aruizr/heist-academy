using System.Linq;
using DG.Tweening;
using MyBox;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class BasicDoor : MonoBehaviour
    {
        [SerializeField] private Transform movingPart;
        [SerializeField] private float forwardRotation;
        [SerializeField] private float backwardRotation;
        [SerializeField] private float rotationTime;
        [SerializeField] private Ease rotationEase;
        [SerializeField] private bool isLocked;

        [SerializeField] [ConditionalField(nameof(isLocked))]
        private GameObjectRuntimeSet playerKeyInventory;

        public UnityEvent onStartedOpening;
        public UnityEvent onFinishedOpening;
        public UnityEvent onStartedClosing;
        public UnityEvent onFinishedClosing;

        [ConditionalField(nameof(isLocked))] public UnityEvent onLocked;
        [ConditionalField(nameof(isLocked))] public UnityEvent onUnlocked;

        private Plane _doorPlane;
        private Vector3 _originalRotation;

        public bool IsOpen { get; private set; }

        private void Awake()
        {
            var t = transform;
            _doorPlane = new Plane(t.forward, t.position);
            _originalRotation = movingPart.eulerAngles;
        }

        public void Open(Transform source)
        {
            if (isLocked)
            {
                if (playerKeyInventory.Any())
                {
                    playerKeyInventory.Remove(playerKeyInventory.ElementAt(0));
                    isLocked = false;
                    onUnlocked?.Invoke();
                    ForceOpen(source);
                }
                else
                {
                    onLocked?.Invoke();
                }
            }
            else
            {
                ForceOpen(source);
            }
        }

        public void ForceOpen(Transform source)
        {
            if (IsOpen) return;
            IsOpen = true;
            onStartedOpening?.Invoke();
            if (_doorPlane.GetSide(source.position))
                movingPart.DORotate(_originalRotation + new Vector3(0, forwardRotation, 0), rotationTime)
                    .SetEase(rotationEase).OnComplete(() => onFinishedOpening?.Invoke());
            else
                movingPart.DORotate(_originalRotation + new Vector3(0, backwardRotation, 0), rotationTime)
                    .SetEase(rotationEase).OnComplete(() => onFinishedOpening?.Invoke());
        }

        public void Close()
        {
            if (!IsOpen) return;
            IsOpen = false;
            onStartedClosing?.Invoke();
            movingPart.DORotate(_originalRotation, rotationTime).SetEase(rotationEase)
                .OnComplete(() => onFinishedClosing?.Invoke());
        }
    }
}