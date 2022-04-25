using Codetox.Attributes;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

namespace AI
{
    public class ChaseController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] [Min(0)] private float maxRange;
        [SerializeField] [Disabled] private GameObject target;
        [SerializeField] private UnityEvent onTargetLost;

        private void Update()
        {
            if (!target) return;
            
            navMeshAgent.SetDestination(target.transform.position);

            if (navMeshAgent.remainingDistance > maxRange || !navMeshAgent.CanReachDestination())
            {
                StopChasing();
                onTargetLost?.Invoke();
            }
        }

        public void StartChasing(GameObject target)
        {
            this.target = target;
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.acceleration = acceleration;
        }

        public void StopChasing()
        {
            target = null;
            navMeshAgent.SetDestination(transform.position);
        }
    }
}