using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;

namespace AI
{
    public class RoutePatrolController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private RouteSpot[] route;
        [SerializeField] private UnityEvent onRouteComplete;

        private int _index;

        private void OnEnable()
        {
            StartCoroutine(DoPatrol());
        }

        private void OnDisable()
        {
            StopCoroutine(DoPatrol());
            IncrementIndex();
        }

        private IEnumerator DoPatrol()
        {
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.acceleration = acceleration;
            while (true)
            {
                var spot = route[_index];
                navMeshAgent.SetDestination(spot.transform.position);
                yield return new WaitUntil(() => navMeshAgent.HasReachedDestination());
                var isRotated = false;
                navMeshAgent.transform.DORotate(spot.transform.eulerAngles, 1).OnComplete(() => isRotated = true);
                yield return new WaitUntil(() => isRotated);
                yield return new WaitForSeconds(spot.time);
                IncrementIndex();
            }
        }

        private void IncrementIndex()
        {
            _index++;
            if (_index < route.Length) return;
            _index = 0;
            onRouteComplete.Invoke();
        }
    }
}