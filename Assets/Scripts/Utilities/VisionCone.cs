using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codetox.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class VisionCone : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] [Range(0, 360)] private float fieldOfView;
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private LayerMask obstacleLayers;
        [SerializeField] private float scanRate;
        [SerializeField] private Color gizmosColor = Color.white;
        [SerializeField] [Min(1)] private int gizmosConeDetail = 1;
        [SerializeField] private UnityEvent<GameObject> onTargetDetected;
        [SerializeField] private UnityEvent<GameObject> onTargetUndetected;

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
            var scannedObjects = Physics.OverlapSphere(_transform.position, distance, targetLayers)
                .Select(coll => coll.gameObject).ToList();

            for (var i = VisibleObjects.Count - 1; i >= 0; i--)
            {
                var obj = VisibleObjects[i];
                if (scannedObjects.Contains(obj)) continue;
                VisibleObjects.RemoveAt(i);
                onTargetUndetected.Invoke(obj);
            }

            foreach (var obj in scannedObjects)
            {
                var isVisible = IsInFieldOfView(obj) && !IsBlockedByObstacle(obj);
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

        private bool IsBlockedByObstacle(GameObject target)
        {
            var currentPosition = _transform.position;
            var directionToTarget = _transform.DirectionTo(target);
            var rayToTarget = new Ray(currentPosition, directionToTarget);
            var distanceToTarget = _transform.DistanceTo(target);
            var combinedLayerMasks = targetLayers | obstacleLayers;

            if (!Physics.Raycast(rayToTarget, out var hit, distanceToTarget, combinedLayerMasks)) return false;

            return !hit.transform.gameObject.Equals(target);
        }

        private bool IsInFieldOfView(GameObject target)
        {
            var currentForward = _transform.forward;
            var vectorToTarget = _transform.VectorTo(target);

            return Vector3.Angle(currentForward, vectorToTarget) <= fieldOfView * 0.5f;
        }
    }
}