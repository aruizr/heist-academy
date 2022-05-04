using UnityEngine;
using UnityEngine.Events;

namespace Interactions.Final
{
    public class Selectible : MonoBehaviour, ISelectible
    {
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onUnselected;

        public virtual void Select()
        {
            onSelected?.Invoke();
        }

        public virtual void Unselect()
        {
            onUnselected?.Invoke();
        }
    }
}