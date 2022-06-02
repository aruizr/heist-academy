using System.Collections.Generic;
using Codetox.Core;
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

        private readonly List<GameObject> _selection = new List<GameObject>();

        private Vector3 _lastPlayerPosition;
        private Vector3 _lastPlayerRotation;

        private void Awake()
        {
            _lastPlayerPosition = playerTransform.position;
            _lastPlayerRotation = playerTransform.eulerAngles;
        }

        private void Update()
        {
            if (_selection.Count <= 1) return;
            if (_selection[0].TryGetComponent<Grabbable>(out var grabbable) && grabbable.IsGrabbed) return;

            var playerPosition = playerTransform.position;
            var playerRotation = playerTransform.eulerAngles;

            if (playerPosition == _lastPlayerPosition && playerRotation == _lastPlayerRotation) return;

            _lastPlayerPosition = playerPosition;
            _lastPlayerRotation = playerRotation;

            var currentSelected = _selection[0];

            _selection.Sort(CompareByProximity);

            var newSelected = _selection[0];

            if (newSelected.Equals(currentSelected)) return;

            currentSelected.Send<ISelectable>(s => s.Unselect());
            newSelected.Send<ISelectable>(s => s.Select());
        }

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

                _selection.Add(o);
            });
        }

        public void Unselect(GameObject o)
        {
            o.Send<ISelectable>(selectable =>
            {
                if (_selection.Count == 0) return;
                if (_selection[0].Equals(o))
                {
                    selectable.Unselect();
                    _selection.RemoveAt(0);

                    switch (_selection.Count)
                    {
                        case 0:
                            return;
                        case 1:
                            onSingleSelection.Invoke();
                            break;
                    }

                    _selection[0].Send<ISelectable>(s => s.Select());
                    return;
                }

                _selection.Remove(o);

                if (_selection.Count == 1) onSingleSelection.Invoke();
            });
        }

        public void Interact()
        {
            if (_selection.Count == 0) return;

            var o = _selection[0];

            o.Send<IInteractive>(interactable => interactable.Interact());
            o.Send<IGrabbable>(grabbable => grabbable.ToggleGrabDrop(handTransform));
        }

        public void Throw()
        {
            if (_selection.Count == 0) return;

            _selection[0].Send<IThrowable>(throwable =>
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

            var o = _selection[0];

            o.Send<ISelectable>(selectable => selectable.Unselect());

            _selection.RemoveAt(0);
            _selection.Add(o);
            _selection[0].Send<ISelectable>(selectable => selectable.Select());
        }

        private int CompareByProximity(GameObject x, GameObject y)
        {
            var px = GetProximityToPlayer(x);
            var py = GetProximityToPlayer(y);

            return px.CompareTo(py);
        }

        private float GetProximityToPlayer(GameObject obj)
        {
            var directionToObj = playerTransform.DirectionTo(obj);
            var distanceToObj = playerTransform.DistanceTo(obj);

            return distanceToObj * Vector3.Angle(playerTransform.forward, directionToObj);
        }
    }
}