using DG.Tweening;
using UnityEngine;

namespace Interactions
{
    public class Gate : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private Vector3 movementOffset;

        [SerializeField] private float movementTime;
        [SerializeField] private Ease movementEasing;

        [Header("Rotation")] [SerializeField] private Vector3 rotationOffset;

        [SerializeField] private float rotationTime;
        [SerializeField] private Ease rotationEasing;

        private bool _isOpen;
        private Vector3 _originalPosition;
        private Vector3 _originalRotation;

        private void Awake()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.eulerAngles;
        }

        public void Reset()
        {
            transform.position = _originalPosition;
            transform.eulerAngles = _originalRotation;
        }

        public void Toggle()
        {
            var finalPosition = _isOpen ? _originalPosition : _originalPosition + movementOffset;
            var finalRotation = _isOpen ? _originalRotation : _originalRotation + rotationOffset;
            _isOpen = !_isOpen;
            transform.DOMove(finalPosition, movementTime).SetEase(movementEasing);
            transform.DORotate(finalRotation, rotationTime).SetEase(rotationEasing);
        }

        public void Open()
        {
            if (_isOpen) return;
            _isOpen = true;
            transform.DOMove(_originalPosition + movementOffset, movementTime).SetEase(movementEasing);
            transform.DORotate(_originalRotation + rotationOffset, rotationTime).SetEase(rotationEasing);
        }

        public void Close()
        {
            if (!_isOpen) return;
            _isOpen = false;
            transform.DOMove(_originalPosition, movementTime).SetEase(movementEasing);
            transform.DORotate(_originalRotation, rotationTime).SetEase(rotationEasing);
        }
    }
}