using Codetox.Core;
using Codetox.GameEvents;
using UnityEngine;
using Variables;

namespace AI.Camera
{
    public class TargetingController : MonoBehaviour
    {
        [SerializeField] private Transform controlTransform;
        [SerializeField] private ValueReference<float> rotationSpeed;
        [SerializeField] private GameObjectGameEvent playerDetectedEvent;

        private GameObject _target;

        private void Update()
        {
            if (!_target) return;

            var directionToTarget = controlTransform.DirectionTo(_target);
            var targetRotation = Quaternion.LookRotation(directionToTarget);
            var deltaDegrees = rotationSpeed.Value * Time.deltaTime;

            controlTransform.rotation = Quaternion.RotateTowards(controlTransform.rotation, targetRotation, deltaDegrees);
        }

        private void OnDisable()
        {
            StopLookingAtTarget();
        }

        public void PlayerDetected()
        {
            if (playerDetectedEvent) playerDetectedEvent.Invoke(_target);
        }

        public void StartLookingAtTarget(GameObject target)
        {
            _target = target;
        }

        public void StopLookingAtTarget()
        {
            _target = null;
        }
    }
}