using Codetox.Attributes;
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
        [Disabled] public GameObject target;

        public UnityEvent onTargetLost;

        private void Update()
        {
            if (!target) return;
            if (!IsTargetInRange())
            {
                onTargetLost?.Invoke();
                return;
            }

            var directionToTarget = controlTransform.DirectionTo(target);
            var currentRotation = controlTransform.rotation;
            var rotationToTarget = Quaternion.LookRotation(directionToTarget);
            var deltaDegrees = rotationSpeed.Value * Time.deltaTime;

            controlTransform.rotation = Quaternion.RotateTowards(currentRotation, rotationToTarget, deltaDegrees);
        }

        private void OnDisable()
        {
            target = null;
        }

        public void PlayerDetected()
        {
            if (target && playerDetectedEvent) playerDetectedEvent.Invoke(target);
        }

        private bool IsTargetInRange()
        {
            return range.Value >= 0 || controlTransform.DistanceTo(target) <= range.Value;
        }
    }
}