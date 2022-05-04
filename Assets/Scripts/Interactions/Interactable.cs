using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Interactable : Selectible, IInteractable
    {
        [SerializeField] private UnityEvent onInteracted;

        public void Interact()
        {
            onInteracted?.Invoke();
        }
    }
}