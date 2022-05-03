using UnityEngine;
using UnityEngine.Events;

namespace Interactions.Final
{
    public class Selectible : MonoBehaviour, ISelectible
    {
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onUnselected;

        public void Select()
        {
            onSelected?.Invoke();
        }

        public void Unselect()
        {
            onUnselected?.Invoke();
        }
    }
}