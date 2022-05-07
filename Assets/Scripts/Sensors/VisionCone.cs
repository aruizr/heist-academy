using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codetox.Core;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Sensors
{
    public class VisionCone : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] [Range(0, 360)] private float fieldOfView;
        [SerializeField] private ValueReference<LayerMask> targetLayers;
        [SerializeField] private ValueReference<LayerMask> obstacleLayers;
        [SerializeField] private CollisionDetectionStrategy detectionStrategy;
        [SerializeField] private float scanRate = 10;
        [SerializeField] private Color gizmosColor = Color.white;
        [SerializeField] [Min(1)] private int gizmosConeDetail = 1;

        public UnityEvent<GameObject> onTargetDetected;
        public UnityEvent<GameObject> onTargetUndetected;

        public readonly List<GameObject> VisibleObjects = new List<GameObject>();

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            StartCoroutine(ScanCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(ScanCoroutine());
        }

        private IEnumerator ScanCoroutine()
        {
            while (true)
            {
                Scan();
                yield return new WaitForSeconds(1 / scanRate);
            }
        }

        private void Scan()
        {
            var scannedColliders = Physics.
                OverlapSphere(_transform.position, distance, targetLayers.Value).
                Where(IsValid).
                ToArray();
            
            var scannedObjects = scannedColliders.
                Select(coll => coll.gameObject).
                ToArray();

            for (var i = VisibleObjects.Count - 1; i >= 0; i--)
            {
                var obj = VisibleObjects[i];
                if (scannedObjects.Contains(obj)) continue;
                VisibleObjects.RemoveAt(i);
                onTargetUndetected.Invoke(obj);
            }

            foreach (var coll in scannedColliders)
            {
                var obj = coll.gameObject;
                var isVisible = IsInFieldOfView(coll) && !IsBlockedByObstacle(coll);
                var isAlreadySeen = VisibleObjects.Contains(obj);

                switch (isVisible)
                {
                    case true when !isAlreadySeen:
                        VisibleObjects.Add(obj);
                        onTargetDetected.Invoke(obj);
                        break;
                    case false when isAlreadySeen:
                        VisibleObjects.Remove(obj);
                        onTargetUndetected.Invoke(obj);
                        break;
                }
            }
        }

        private bool IsBlockedByObstacle(Collider target)
        {
            var currentPosition = _transform.position;
            var closestPoint = target.ClosestPoint(currentPosition);
            var directionToTarget = _transform.DirectionTo(closestPoint);
            var rayToTarget = new Ray(currentPosition, directionToTarget);
            var distanceToTarget = _transform.DistanceTo(closestPoint);

            // if (!Physics.Raycast(rayToTarget, out var hit, distanceToTarget, obstacleLayers.Value)) return false;
            //
            // var distanceToObstacle = _transform.DistanceTo(hit.point);
            //
            // return distanceToObstacle < distanceToTarget;

            return Physics.Raycast(rayToTarget, distanceToTarget, obstacleLayers.Value);
        }

        private bool IsInFieldOfView(Collider target)
        {
            var currentPosition = _transform.position;
            var closestPoint = target.ClosestPoint(currentPosition);
            var currentForward = _transform.forward;
            var vectorToTarget = _transform.VectorTo(closestPoint);

            return Vector3.Angle(currentForward, vectorToTarget) <= fieldOfView * 0.5f;
        }

        private bool IsValid(Collider coll)
        {
            var isValidType = coll is SphereCollider || coll is BoxCollider || coll is CapsuleCollider;
            var isValidForStrategy = coll.isTrigger switch
            {
                true when detectionStrategy == CollisionDetectionStrategy.Colliders => false,
                false when detectionStrategy == CollisionDetectionStrategy.TriggerColliders => false,
                _ => true
            };
            return isValidType && isValidForStrategy;
        }
    }
}