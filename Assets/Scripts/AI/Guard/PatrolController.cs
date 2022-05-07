using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Utilities;
using Variables;

namespace AI.Guard
{
    public class PatrolController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<float> movementSpeed;
        [SerializeField] private ValueReference<float> acceleration;
        [SerializeField] private RouteStep[] route;

        private Tweener _currentTween;

        private void OnEnable()
        {
            StartCoroutine(DoPatrol());
        }

        private void OnDisable()
        {
            StopCoroutine(DoPatrol());
            _currentTween?.Kill();
        }

        private IEnumerator DoPatrol()
        {
            if (route.Length == 0) yield return new WaitUntil(() => route.Length > 0);
            foreach (var step in route.WrappedAround())
            {
                navMeshAgent.speed = movementSpeed.Value;
                navMeshAgent.acceleration = acceleration.Value;
                navMeshAgent.SetDestination(step.transform.position);

                yield return new WaitUntil(() => navMeshAgent.HasReachedDestination());

                var isRotationComplete = false;

                _currentTween?.Kill();
                _currentTween = navMeshAgent.transform.
                    DORotate(step.transform.eulerAngles, 1).
                    OnComplete(() => isRotationComplete = true);

                yield return new WaitUntil(() => isRotationComplete);
                yield return new WaitForSeconds(step.time);
            }
        }

        [Serializable]
        public struct RouteStep
        {
            public Transform transform;
            public float time;
        }
    }
}