using System.Linq;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Lock : MonoBehaviour
    {
        [SerializeField] private GameObject unlockedBy;
        [SerializeField] private GameObjectRuntimeSet playerInventory;
        [SerializeField] private UnityEvent onUnlocked;
        [SerializeField] private UnityEvent onUnlockFailed;

        public void Unlock()
        {
            if (playerInventory.Contains(unlockedBy)) onUnlocked?.Invoke();
            else onUnlockFailed?.Invoke();
        }
    }
}