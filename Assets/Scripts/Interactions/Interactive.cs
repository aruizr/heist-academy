using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Interactive : Selectable, IInteractive
    {
        [SerializeField] private UnityEvent onInteracted;

        public void Interact()
        {
            onInteracted?.Invoke();
        }
    }
}