using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Grabbeable : Selectible, IGrabbeable, IThroweable
    {
        [SerializeField] private new Rigidbody rigidbody;

        public UnityEvent onGrabbed;
        public UnityEvent onDropped;
        public UnityEvent onThrown;

        private bool _isGrabbed;
        private bool _isKinematic;
        private Transform _toFollow;

        private void Awake()
        {
            _isKinematic = rigidbody.isKinematic;
        }

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
            rigidbody.isKinematic = _isKinematic;
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
            rigidbody.isKinematic = _isKinematic;
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