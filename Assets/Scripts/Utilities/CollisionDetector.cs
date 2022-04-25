using Codetox.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayers;
        [SerializeField] private CollisionDetectionStrategy detectionStrategy;
        [SerializeField] private UnityEvent<GameObject> onEnter;
        [SerializeField] private UnityEvent<GameObject> onExit;

        private void OnCollisionEnter(Collision other)
        {
            if (detectionStrategy == CollisionDetectionStrategy.TriggerColliders) return;
            OnEnter(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            if (detectionStrategy == CollisionDetectionStrategy.TriggerColliders) return;
            OnExit(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (detectionStrategy == CollisionDetectionStrategy.Colliders) return;
            OnEnter(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (detectionStrategy == CollisionDetectionStrategy.Colliders) return;
            OnExit(other.gameObject);
        }

        private void OnEnter(GameObject obj)
        {
            if (!obj.IsInLayerMask(targetLayers)) return;
            onEnter?.Invoke(obj);
        }

        private void OnExit(GameObject obj)
        {
            if (!obj.IsInLayerMask(targetLayers)) return;
            onExit?.Invoke(obj);
        }
    }
}