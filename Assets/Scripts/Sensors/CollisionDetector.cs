using Codetox.Core;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Sensors
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private ValueReference<LayerMask> targetLayers;
        [SerializeField] private CollisionDetectionStrategy detectionStrategy;
        
        public UnityEvent<GameObject> onEnter;
        public UnityEvent<GameObject> onExit;

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
            if (!obj.IsInLayerMask(targetLayers.Value)) return;
            onEnter?.Invoke(obj);
        }

        private void OnExit(GameObject obj)
        {
            if (!obj.IsInLayerMask(targetLayers.Value)) return;
            onExit?.Invoke(obj);
        }
    }
}