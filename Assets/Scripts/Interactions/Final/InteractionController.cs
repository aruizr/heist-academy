using System.Collections.Generic;
using System.Linq;
using Codetox.Messaging;
using UnityEngine;
using Variables;

namespace Interactions.Final
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private ValueReference<Vector2> throwingVelocity;

        private Queue<GameObject> _selection = new Queue<GameObject>();

        public void Select(GameObject o)
        {
            o.Send<ISelectible>(selectible =>
            {
                if (_selection.Count == 0) selectible.Select();
                _selection.Enqueue(o);
            });
        }

        public void Unselect(GameObject o)
        {
            o.Send<ISelectible>(selectible =>
            {
                if (_selection.Count == 0) return;
                if (_selection.Peek().Equals(o))
                {
                    selectible.Unselect();
                    _selection.Dequeue();
                    if (_selection.Count == 0) return;
                    _selection.Peek().Send<ISelectible>(s => s.Select());
                    return;
                }

                _selection = new Queue<GameObject>(_selection.Where(obj => !obj.Equals(o)));
            });
        }

        public void Interact()
        {
            if (_selection.Count == 0) return;
            var o = _selection.Peek();
            o.Send<IInteractable>(interactable => interactable.Interact());
            o.Send<IGrabbeable>(grabbeable => grabbeable.ToggleGrabDrop(handTransform));
        }

        public void Throw()
        {
            if (_selection.Count == 0) return;
            _selection.Peek().Send<IThroweable>(throweable =>
            {
                var forward = playerTransform.forward;
                var velocity = throwingVelocity.Value;
                var x = forward.x * velocity.x;
                var y = velocity.y;
                var z = forward.z * velocity.x;
                var finalVelocity = new Vector3(x, y, z);
                throweable.Throw(finalVelocity);
            });
        }

        public void Switch()
        {
            if (_selection.Count == 0) return;
            var o = _selection.Dequeue();
            o.Send<ISelectible>(selectible => selectible.Unselect());
            _selection.Enqueue(o);
            _selection.Peek().Send<ISelectible>(selectible => selectible.Select());
        }
    }
}