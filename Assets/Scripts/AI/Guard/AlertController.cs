using Codetox.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities;
using Variables;

namespace AI.Guard
{
    public class AlertController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<float> movementSpeed;
        [SerializeField] private ValueReference<float> acceleration;
        [SerializeField] private ValueReference<float> turningSpeed;

        public UnityEvent onDestinationReached;
        
        private Transform _transform;
        private Tweener _currentTween;

        private void Update()
        {
            if (navMeshAgent.HasReachedDestination()) onDestinationReached?.Invoke();
        }

        private void OnEnable()
        {
            _transform = navMeshAgent.transform;
        }

        private void OnDisable()
        {
            _currentTween?.Kill();
            navMeshAgent.isStopped = false;
        }
        
        public void SetAlert(GameObject target)
        {
            SetAlert(target.transform.position);
        }

        public void SetAlert(Vector3 point)
        {
            var destination = new Vector3(point.x, _transform.position.y, point.z);
            var vectorToDestination = _transform.VectorTo(destination);
            var angleToDestination = Vector3.Angle(_transform.forward, vectorToDestination);
            var timeToRotate = angleToDestination / turningSpeed.Value;

            navMeshAgent.speed = movementSpeed.Value;
            navMeshAgent.acceleration = acceleration.Value;
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(point);
            _currentTween?.Kill();
            _currentTween = _transform.
                DOLookAt(destination, timeToRotate).
                OnComplete(() => navMeshAgent.isStopped = false);
        }
    }
}