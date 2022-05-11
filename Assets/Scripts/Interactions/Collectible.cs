using RuntimeSets;
using UnityEngine;

namespace Interactions
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet inventory;

        private void OnDestroy()
        {
            inventory.Remove(gameObject);
        }

        public void Collect()
        {
            inventory.Add(gameObject);
        }
    }
}