using System.Collections.Generic;
using System.Linq;
using Codetox.Messaging;
using Interactions;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private ValueReference<Vector2> throwingVelocity;

        public UnityEvent onMultipleSelection;
        public UnityEvent onSingleSelection;

        private Queue<GameObject> _selection = new Queue<GameObject>();

        public void Select(GameObject o)
        {
            o.Send<ISelectable>(selectable =>
            {
                switch (_selection.Count)
                {
                    case 0:
                        selectable.Select();
                        break;
                    case 1:
                        onMultipleSelection?.Invoke();
                        break;
                }

                _selection.Enqueue(o);
            });
        }

        public void Unselect(GameObject o)
        {
            o.Send<ISelectable>(selectable =>
            {
                if (_selection.Count == 0) return;
                if (_selection.Peek().Equals(o))
                {
                    selectable.Unselect();
                    _selection.Dequeue();

                    switch (_selection.Count)
                    {
                        case 0:
                            return;
                        case 1:
                            onSingleSelection.Invoke();
                            break;
                    }

                    _selection.Peek().Send<ISelectable>(s => s.Select());
                    return;
                }

                _selection = new Queue<GameObject>(_selection.Where(obj => !obj.Equals(o)));
                if (_selection.Count == 1) onSingleSelection.Invoke();
            });
        }

        public void Interact()
        {
            if (_selection.Count == 0) return;
            var o = _selection.Peek();
            o.Send<IInteractive>(interactable => interactable.Interact());
            o.Send<IGrabbable>(grabbable => grabbable.ToggleGrabDrop(handTransform));
        }

        public void Throw()
        {
            if (_selection.Count == 0) return;
            _selection.Peek().Send<IThrowable>(throwable =>
            {
                var forward = playerTransform.forward;
                var velocity = throwingVelocity.Value;
                var x = forward.x * velocity.x;
                var y = velocity.y;
                var z = forward.z * velocity.x;
                var finalVelocity = new Vector3(x, y, z);
                throwable.Throw(finalVelocity);
            });
        }

        public void SwitchSelection()
        {
            if (_selection.Count == 0) return;
            var o = _selection.Dequeue();
            o.Send<ISelectable>(selectable => selectable.Unselect());
            _selection.Enqueue(o);
            _selection.Peek().Send<ISelectable>(selectable => selectable.Select());
        }
    }
}