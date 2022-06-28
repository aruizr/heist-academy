using System.Collections.Generic;
using Codetox.Messaging;
using PropertyAnimators;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class KeysDisplayController : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet inventory;
        [SerializeField] private GameObject parentElement;
        [SerializeField] private GameObject keyElement;

        public UnityEvent onKeyCollected;

        private List<GameObject> _displayed = new List<GameObject>();

        private void OnEnable()
        {
            inventory.OnItemAdded += OnItemAdded;
            inventory.OnItemRemoved += OnItemRemoved;
        }

        private void OnDisable()
        {
            inventory.OnItemAdded -= OnItemAdded;
            inventory.OnItemRemoved -= OnItemRemoved;
        }

        private void OnItemRemoved(GameObject obj)
        {
            if (parentElement.transform.childCount == 0) return;
            Destroy(parentElement.transform.GetChild(0).gameObject);
        }

        private void OnItemAdded(GameObject obj)
        {
            var key = Instantiate(keyElement, parentElement.transform);
            _displayed.Add(key);
            key.Send<PropertyAnimator<Transform, Vector3>>(animator => animator.PlayForward());
        }
    }
}