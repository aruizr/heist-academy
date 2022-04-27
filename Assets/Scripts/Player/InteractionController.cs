using Codetox.Messaging;
using Codetox.Variables;
using Interactions;
using UnityEngine;

namespace Player
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector2Variable throwingVelocity;
        
        private IInteractable _current;
        
        private bool _isGrabbed;

        public void Select(GameObject obj)
        {
            if (_current != null) return;
            obj.Send<IInteractable>(interactable =>
            {
                interactable.Select();
                _current = interactable;
            });
        }

        public void Unselect(GameObject obj)
        {
            if (_current == null) return;
            obj.Send<IInteractable>(interactable =>
            {
                if (!interactable.Equals(_current)) return;
                interactable.Unselect();
                _current = null;
            });
        }

        public void Interact()
        {
            _current?.Interact();
            ToggleGrabDrop();
        }
        
        public void ToggleGrabDrop()
        {
            if (_current == null)
            {
                _isGrabbed = false;
                return;
            }

            if (!(_current is IGrabbeable grabbeable)) return;

            if (_isGrabbed)
                grabbeable.Drop();
            else
                grabbeable.Grab(handTransform);

            _isGrabbed = !_isGrabbed;
        }
        
        public void Throw()
        {
            if (_current == null)
            {
                _isGrabbed = false;
                return;
            }

            if (!_isGrabbed) return;
            if (!(_current is IGrabbeable grabbeable)) return;

            var forward = playerTransform.forward;
            var velocity = throwingVelocity.Value;
            var x = forward.x * velocity.x;
            var y = velocity.y;
            var z = forward.z * velocity.x;
            grabbeable.Throw(new Vector3(x, y, z));
            _isGrabbed = false;
        }
    }
}