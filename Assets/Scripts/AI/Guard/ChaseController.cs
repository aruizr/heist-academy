using Codetox.Attributes;
using Codetox.Core;
using RuntimeSets;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;
using Variables;

namespace AI.Guard
{
    public class ChaseController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<float> movementSpeed;
        [SerializeField] private ValueReference<float> acceleration;
        [SerializeField] private ValueReference<float> maxRange;
        [SerializeField] private ValueReference<float> rangeToBustTarget;
        [SerializeField] [Disabled] private GameObject target;

        public UnityEvent<GameObject> onTargetLost;
        public UnityEvent onTargetBusted;

        private Transform _controlTransform;

        private void Update()
        {
            if (!target) return;

            var distance = _controlTransform.DistanceTo(target);

            if (!navMeshAgent.isStopped && distance > maxRange.Value || !navMeshAgent.CanReachDestination())
            {
                var target = this.target;
                this.target = null;
                onTargetLost?.Invoke(target);
                return;
            }

            if (distance <= rangeToBustTarget.Value)
            {
                onTargetBusted?.Invoke();
                return;
            }

            navMeshAgent.SetDestination(target.transform.position);
        }

        private void OnEnable()
        {
            _controlTransform = navMeshAgent.transform;
        }

        public void StartChasing(GameObject target)
        {
            this.target = target;
            navMeshAgent.speed = movementSpeed.Value;
            navMeshAgent.acceleration = acceleration.Value;
        }

        public void StopChasing()
        {
            target = null;
            //navMeshAgent.SetDestination(transform.position);
        }
    }
}