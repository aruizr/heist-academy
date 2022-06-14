using Codetox.Messaging;
using RuntimeSets;
using TMPro;
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
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textPlaceholder;

        private void Awake()
        {
            image.sprite = slotSprite;
            if (text) text.text = textPlaceholder;
        }

        private void OnEnable()
        {
            inventory.OnItemAdded += OnItemAdded;

            foreach (var o in inventory)
            {
                o.Send<Identifier>(identifier =>
                {
                    if (!identifier.ID.Equals(collectibleID.Value)) return;
                    image.sprite = slotFilledSprite;
                    if (text) text.text = identifier.ID;
                });
            }
        }

        private void OnDisable()
        {
            inventory.OnItemAdded -= OnItemAdded;
        }

        private void OnItemAdded(GameObject obj)
        {
            obj.Send<Identifier>(identifier =>
            {
                if (!identifier.ID.Equals(collectibleID.Value)) return;
                image.sprite = slotFilledSprite;
                if (text) text.text = identifier.ID;
            });
        }
    }
}