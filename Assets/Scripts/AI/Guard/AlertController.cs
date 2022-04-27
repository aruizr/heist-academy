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
        [SerializeField] private ValueReference<float> movingSpeed;
        [SerializeField] private ValueReference<float> acceleration;
        [SerializeField] private ValueReference<float> turningSpeed;
        [SerializeField] private UnityEvent onDestinationReached;

        private Tweener _currentTween;

        private void Update()
        {
            if (navMeshAgent.HasReachedDestination()) onDestinationReached?.Invoke();
        }

        private void OnDisable()
        {
            _currentTween?.Kill();
            navMeshAgent.isStopped = false;
        }

        public void SetAlert(Vector3 position)
        {
            var t = navMeshAgent.transform;
            var transformPosition = t.position;
            var horizontalPosition = new Vector3(position.x, transformPosition.y, position.z);
            var direction = t.DirectionTo(horizontalPosition);
            var angle = Vector3.Angle(t.forward, direction);
            var time = angle / turningSpeed.Value;

            navMeshAgent.speed = movingSpeed.Value;
            navMeshAgent.acceleration = acceleration.Value;
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(position);
            _currentTween?.Kill();
            _currentTween = t.DOLookAt(horizontalPosition, time).OnComplete(() => navMeshAgent.isStopped = false);
        }

        public void SetAlert(GameObject target)
        {
            SetAlert(target.transform);
        }

        public void SetAlert(Transform target)
        {
            SetAlert(target.position);
        }
    }
}