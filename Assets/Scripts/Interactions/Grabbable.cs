using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Grabbable : Selectable, IGrabbable, IThrowable
    {
        [SerializeField] private new Rigidbody rigidbody;

        public UnityEvent onGrabbed;
        public UnityEvent onDropped;
        public UnityEvent onThrown;

        private bool _isGrabbed;
        private Transform _toFollow;

        private void FixedUpdate()
        {
            if (!_toFollow) return;
            rigidbody.MovePosition(_toFollow.position);
            rigidbody.MoveRotation(_toFollow.rotation);
        }

        public void ToggleGrabDrop(Transform parent)
        {
            if (_isGrabbed) Drop();
            else Grab(parent);
        }

        public void Throw(Vector3 velocity)
        {
            if (!_isGrabbed) return;
            _toFollow = null;
            rigidbody.isKinematic = false;
            rigidbody.velocity += velocity;
            _isGrabbed = false;
            onThrown?.Invoke();
        }

        private void Grab(Transform parent)
        {
            if (_isGrabbed) return;
            _toFollow = parent;
            rigidbody.isKinematic = true;
            _isGrabbed = true;
            onGrabbed?.Invoke();
        }

        private void Drop()
        {
            if (!_isGrabbed) return;
            _toFollow = null;
            rigidbody.isKinematic = false;
            _isGrabbed = false;
            onDropped?.Invoke();
        }

        public override void Unselect()
        {
            Drop();
            base.Unselect();
        }
    }
}