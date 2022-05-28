using Codetox.Messaging;
using RuntimeSets;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Variables;

namespace UI
{
    public class CollectibleDisplayController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite slotSprite;
        [SerializeField] private Sprite slotFilledSprite;
        [SerializeField] private ValueReference<string> collectibleID;
        [SerializeField] private GameObjectRuntimeSet inventory;

        private void Awake()
        {
            image.sprite = slotSprite;
        }

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
                if (identifier.ID.Equals(collectibleID.Value)) image.sprite = slotFilledSprite;
            });
        }
    }
}