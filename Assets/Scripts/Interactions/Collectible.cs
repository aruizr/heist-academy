using RuntimeSets;
using UnityEngine;

namespace Interactions
{
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet playerInventory;

        private void OnDestroy()
        {
            playerInventory.Remove(gameObject);
        }

        public void Collect()
        {
            playerInventory.Add(gameObject);
        }
    }
}