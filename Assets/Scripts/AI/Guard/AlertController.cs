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

        public UnityEvent onDestinationReached;
        private Transform _controlTransform;

        private Tweener _currentTween;

        private void Update()
        {
            if (navMeshAgent.HasReachedDestination()) onDestinationReached?.Invoke();
        }

        private void OnEnable()
        {
            _controlTransform = navMeshAgent.transform;
        }

        private void OnDisable()
        {
            _currentTween?.Kill();
            navMeshAgent.isStopped = false;
        }

        public void SetAlert(Vector3 point)
        {
            var finalPoint = new Vector3(point.x, _controlTransform.position.y, point.z);
            var direction = _controlTransform.DirectionTo(finalPoint);
            var angle = Vector3.Angle(_controlTransform.forward, direction);
            var time = angle / turningSpeed.Value;

            navMeshAgent.speed = movingSpeed.Value;
            navMeshAgent.acceleration = acceleration.Value;
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(point);
            _currentTween?.Kill();
            _currentTween = _controlTransform.DOLookAt(finalPoint, time)
                .OnComplete(() => navMeshAgent.isStopped = false);
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