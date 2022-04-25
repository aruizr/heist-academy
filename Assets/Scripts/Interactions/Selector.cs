using Codetox.Messaging;
using UnityEngine;

namespace Interactions
{
    public class Selector : MonoBehaviour
    {
        protected ISelectible current;

        public void Select(GameObject target)
        {
            if (!target) return;
            if (current != null) return;
            target.Send<ISelectible>(selectible =>
            {
                selectible.Select();
                current = selectible;
            });
        }

        public void Unselect(GameObject target)
        {
            if (!target) return;
            if (current == null) return;
            target.Send<ISelectible>(selectible =>
            {
                if (!selectible.Equals(current)) return;
                selectible.Unselect();
                current = null;
            });
        }
    }
}