using Codetox.Messaging;
using UnityEngine;

namespace Interactions
{
    public class Interactor : Selector
    {
        public void Interact(GameObject target)
        {
            target.Send<IInteractable>(interactable => interactable.Interact());
        }

        public void Interact()
        {
            (current as IInteractable)?.Interact();
        }
    }
}