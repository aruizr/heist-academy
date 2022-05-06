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

        public UnityEvent onTargetLost;
        public UnityEvent onTargetBusted;

        private Transform _transform;

        public GameObject Target { get; set; }

        private void Update()
        {
            if (!Target) return;
            if (navMeshAgent.isStopped) return;

            if (!CanReachTarget())
            {
                Target = null;
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
            navMeshAgent.SetDestination(Target.transform.position);
        }

        private void OnEnable()
        {
            _transform = navMeshAgent.transform;
        }

        private void OnDisable()
        {
            Target = null;
        }

        private bool CanReachTarget()
        {
            var isTargetInRange = _transform.DistanceTo(Target) <= chasingRange.Value;
            var isTargetReachable = navMeshAgent.CanReachDestination();

            return isTargetInRange && isTargetReachable;
        }

        private bool CanBustTarget()
        {
            return _transform.DistanceTo(Target) <= bustingRange.Value;
        }
    }
}