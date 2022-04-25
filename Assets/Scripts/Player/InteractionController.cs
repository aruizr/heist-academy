using Codetox.Messaging;
using Interactions;
using UnityEngine;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        private IInteractable _currentInteractable;

        public void Select(GameObject obj)
        {
            if (_currentInteractable != null) return;
            obj.Send<IInteractable>(interactable =>
            {
                interactable.Select();
                _currentInteractable = interactable;
            });
        }

        public void Unselect(GameObject obj)
        {
            obj.Send<IInteractable>(interactable =>
            {
                if (!interactable.Equals(_currentInteractable)) return;
                interactable.Unselect();
                _currentInteractable = null;
            });
        }

        public void Interact()
        {
            _currentInteractable?.Interact();
        }
    }
}