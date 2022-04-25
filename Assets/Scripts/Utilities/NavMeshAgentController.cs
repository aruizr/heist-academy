using Codetox.Attributes;
using Codetox.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Utilities
{
    public class NavMeshAgentController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float maxDestinationRange;
        [SerializeField] [Disabled] private Vector3 currentDestination;
        [SerializeField] [Disabled] private Transform currentTarget;
        [SerializeField] private UnityEvent onDestinationReached;
        [SerializeField] private UnityEvent onDestinationUnreachable;

        private bool _hasReachedDestination;

        private void Update()
        {
            if (currentTarget) SetNavMeshAgentDestination(currentTarget.position);

            if (navMeshAgent.HasReachedDestination() && !_hasReachedDestination)
            {
                _hasReachedDestination = true;
                onDestinationReached?.Invoke();
            }
            else if (!navMeshAgent.HasReachedDestination())
            {
                _hasReachedDestination = false;
            }
        }

        private void SetNavMeshAgentDestination(Vector3 position)
        {
            var path = new NavMeshPath();
            navMeshAgent.CalculatePath(position, path);

            if (path.status != NavMeshPathStatus.PathComplete ||
                navMeshAgent.transform.DistanceTo(position) > maxDestinationRange)
            {
                onDestinationUnreachable?.Invoke();
                return;
            }

            navMeshAgent.speed = movementSpeed;
            navMeshAgent.acceleration = acceleration;
            currentDestination = position;
            navMeshAgent.SetDestination(position);
        }

        public void SetDestination(Vector3 position)
        {
            SetNavMeshAgentDestination(position);
        }

        public void SetDestination(Transform target)
        {
            SetDestination(target.position);
        }

        public void SetDestination(GameObject target)
        {
            SetDestination(target.transform);
        }

        public void Follow(Transform target)
        {
            currentTarget = target;
        }

        public void Follow(GameObject target)
        {
            Follow(target.transform);
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        public void Resume()
        {
            navMeshAgent.isStopped = false;
        }
    }
}