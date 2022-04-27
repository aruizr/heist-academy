using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

namespace AI.Guard
{
    public class IdleController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private Transform destination;
        [SerializeField] private UnityEvent onDestinationReached;

        private bool _hasReachedDestination;

        private void Update()
        {
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.acceleration = acceleration;
            navMeshAgent.SetDestination(destination.position);
            if (navMeshAgent.HasReachedDestination() && !_hasReachedDestination)
            {
                _hasReachedDestination = true;
                onDestinationReached?.Invoke();
                navMeshAgent.transform.DORotate(destination.eulerAngles, 1);
            }
            else if (!navMeshAgent.HasReachedDestination())
            {
                _hasReachedDestination = false;
            }
        }
    }
}