using System;
using Codetox.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float turningSpeed;
        [SerializeField] private Ease ease;
        [SerializeField] private UnityEvent onRotationComplete;
        
        private Tweener _currentTween;

        private void OnDisable()
        {
            _currentTween?.Kill();
        }

        public void LookAt(Vector3 position)
        {
            var horizontalPosition = GetHorizontalPosition(position);
            var angle = GetAngleTo(position);
            var time = GetTimeToRotate(angle);

            _currentTween = target.DOLookAt(horizontalPosition, time).SetEase(ease).OnComplete(() => onRotationComplete?.Invoke());
        }

        public void LookAtPosition(GameObject reference)
        {
            LookAtPosition(reference.transform);
        }

        public void LookAtPosition(Transform reference)
        {
            LookAt(reference.position);
        }

        public void LookDirection(Vector3 direction)
        {
            var position = target.position + direction;
            LookAt(position);
        }

        public void LookDirection(Transform reference)
        {
            LookDirection(reference.forward);
        }

        private Vector3 GetHorizontalPosition(Vector3 position)
        {
            return new Vector3(position.x, target.position.y, position.z);
        }

        private float GetAngleTo(Vector3 position)
        {
            var horizontalPosition = GetHorizontalPosition(position);
            var direction = target.DirectionTo(horizontalPosition);
            return Vector3.Angle(target.forward, direction);
        }

        private float GetTimeToRotate(float angle)
        {
            return angle / turningSpeed;
        }
    }
}