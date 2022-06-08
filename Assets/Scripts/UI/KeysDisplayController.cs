using System.Collections.Generic;
using RuntimeSets;
using UnityEngine;

namespace UI
{
    public class KeysDisplayController : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet inventory;
        [SerializeField] private GameObject parentElement;
        [SerializeField] private GameObject keyElement;

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
            _displayed.Add(Instantiate(keyElement, parentElement.transform));
        }
    }
}