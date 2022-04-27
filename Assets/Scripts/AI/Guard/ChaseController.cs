﻿using Codetox.Attributes;
using Codetox.Core;
using Codetox.Variables;
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
        [SerializeField] private UnityEvent onTargetLost;
        [SerializeField] private UnityEvent onTargetBusted;

        private void Update()
        {
            if (!target) return;
            
            navMeshAgent.SetDestination(target.transform.position);

            if (navMeshAgent.transform.DistanceTo(target) > maxRange.Value || !navMeshAgent.CanReachDestination())
            {
                StopChasing();
                onTargetLost?.Invoke();
            }
            else if (navMeshAgent.transform.DistanceTo(target) <= rangeToBustTarget.Value)
            {
                onTargetBusted?.Invoke();
            }
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
            navMeshAgent.SetDestination(transform.position);
        }
    }
}