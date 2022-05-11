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

        public GameObject Target { get; set; }

        private void Update()
        {
            if (!Target) return;
            if (!IsTargetInRange())
            {
                onTargetLost?.Invoke();
                return;
            }

            var directionToTarget = controlTransform.DirectionTo(Target);
            var currentRotation = controlTransform.rotation;
            var rotationToTarget = Quaternion.LookRotation(directionToTarget);
            var deltaDegrees = rotationSpeed.Value * Time.deltaTime;

            controlTransform.rotation = Quaternion.RotateTowards(currentRotation, rotationToTarget, deltaDegrees);
        }

        private void OnDisable()
        {
            Target = null;
        }

        public void PlayerDetected()
        {
            if (Target && playerDetectedEvent) playerDetectedEvent.Invoke(Target);
        }

        private bool IsTargetInRange()
        {
            if (range.Value <= 0) return true;
            return controlTransform.DistanceTo(Target) <= range.Value;
        }
    }
}