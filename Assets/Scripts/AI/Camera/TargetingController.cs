using Codetox.Core;
using Codetox.GameEvents;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace AI.Camera
{
    public class TargetingController : MonoBehaviour
    {
        [SerializeField] private Transform controlTransform;
        [SerializeField] private ValueReference<float> range;
        [SerializeField] private ValueReference<float> rotationSpeed;
        [SerializeField] private GameObjectGameEvent playerDetectedEvent;

        public UnityEvent onTargetLost;

        public GameObject Target { get; private set; }

        private void Update()
        {
            if (!Target) return;
            if (range.Value > 0 && controlTransform.DistanceTo(Target) > range.Value)
            {
                onTargetLost?.Invoke();
                return;
            }

            var directionToTarget = controlTransform.DirectionTo(Target);
            var targetRotation = Quaternion.LookRotation(directionToTarget);
            var deltaDegrees = rotationSpeed.Value * Time.deltaTime;

            controlTransform.rotation =
                Quaternion.RotateTowards(controlTransform.rotation, targetRotation, deltaDegrees);
        }

        private void OnDisable()
        {
            StopLookingAtTarget();
        }

        public void StartLookingAtTarget(GameObject target)
        {
            Target = target;
        }

        public void StopLookingAtTarget()
        {
            Target = null;
        }

        public void PlayerDetected()
        {
            if (Target && playerDetectedEvent) playerDetectedEvent.Invoke(Target);
        }
    }
}