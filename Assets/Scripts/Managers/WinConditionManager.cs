using Codetox.Messaging;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;
using Utilities;
using Variables;

namespace Managers
{
    public class WinConditionManager : MonoBehaviour
    {
        [SerializeField] private ValueReference<string> collectibleID;
        [SerializeField] private GameObjectRuntimeSet inventory;

        public UnityEvent onGameWon;

        private void OnEnable()
        {
            inventory.OnItemAdded += OnItemAdded;
        }

        private void OnDisable()
        {
            inventory.OnItemAdded -= OnItemAdded;
        }

        private void OnItemAdded(GameObject obj)
        {
            obj.Send<Identifier>(identifier =>
            {
                if (identifier.ID.Equals(collectibleID.Value)) onGameWon?.Invoke();
            });
        }
    }
}