using Codetox.Attributes;
using Codetox.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;
using Variables;

namespace AI.Guard
{
    public class ChasingController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<float> movementSpeed;
        [SerializeField] private ValueReference<float> acceleration;
        [SerializeField] private ValueReference<float> chasingRange;
        [SerializeField] private ValueReference<float> bustingRange;
        [Disabled] public GameObject target;

        public UnityEvent onTargetLost;
        public UnityEvent onTargetBusted;

        private Transform _transform;

        private void Update()
        {
            if (!target) return;
            if (navMeshAgent.isStopped) return;

            if (!CanReachTarget())
            {
                target = null;
                onTargetLost?.Invoke();
                return;
            }

            if (CanBustTarget())
            {
                onTargetBusted?.Invoke();
                return;
            }

            navMeshAgent.speed = movementSpeed.Value;
            navMeshAgent.acceleration = acceleration.Value;
            navMeshAgent.SetDestination(target.transform.position);
        }

        private void OnEnable()
        {
            _transform = navMeshAgent.transform;
        }

        private void OnDisable()
        {
            target = null;
        }

        private bool CanReachTarget()
        {
            var isTargetInRange = _transform.DistanceTo(target) <= chasingRange.Value;
            var isTargetReachable = navMeshAgent.CanReachDestination();

            return isTargetInRange && isTargetReachable;
        }

        private bool CanBustTarget()
        {
            return _transform.DistanceTo(target) <= bustingRange.Value;
        }
    }
}