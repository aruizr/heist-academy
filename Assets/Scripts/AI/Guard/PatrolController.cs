using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private IEnumerator<RouteStep> _enumerator;

        public void Awake()
        {
            _enumerator = route.Cast<RouteStep>().GetEnumerator();
        }

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
            while (true)
            {
                if (route.Length == 0) yield return new WaitUntil(() => route.Length > 0);

                var currentStep = _enumerator.GetWrapAroundNext();

                navMeshAgent.speed = movementSpeed.Value;
                navMeshAgent.acceleration = acceleration.Value;
                navMeshAgent.SetDestination(currentStep.transform.position);

                yield return new WaitUntil(() => navMeshAgent.HasReachedDestination());

                var isRotationComplete = false;

                _currentTween?.Kill();
                _currentTween = navMeshAgent.transform.
                    DORotate(currentStep.transform.eulerAngles, 1).
                    OnComplete(() => isRotationComplete = true);

                yield return new WaitUntil(() => isRotationComplete);
                yield return new WaitForSeconds(currentStep.time.Value);
            }
        }

        [Serializable]
        public struct RouteStep
        {
            public Transform transform;
            public ValueReference<float> time;
        }
    }
}