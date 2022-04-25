using Codetox.Variables;
using Interactions;
using UnityEngine;

namespace Player
{
    public class GrabbingController : Selector
    {
        [SerializeField] private Transform handTransform;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector2Variable throwingVelocity;

        private bool _isGrabbed;

        public void ToggleGrabDrop()
        {
            if (current == null)
            {
                _isGrabbed = false;
                return;
            }

            var currentGrabbeable = current as IGrabbeable;

            if (_isGrabbed)
                currentGrabbeable?.Drop();
            else
                currentGrabbeable?.Grab(handTransform);

            _isGrabbed = !_isGrabbed;
        }

        public void Throw()
        {
            if (current == null)
            {
                _isGrabbed = false;
                return;
            }

            if (!_isGrabbed) return;

            var forward = playerTransform.forward;
            var velocity = throwingVelocity.Value;
            var x = forward.x * velocity.x;
            var y = velocity.y;
            var z = forward.z * velocity.x;
            (current as IGrabbeable)?.Throw(new Vector3(x, y, z));
            _isGrabbed = false;
        }
    }
}