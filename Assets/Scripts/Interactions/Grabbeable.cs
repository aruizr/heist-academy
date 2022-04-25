using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Grabbeable : Selectible, IGrabbeable
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private UnityEvent onGrabbed;
        [SerializeField] private UnityEvent onDropped;
        [SerializeField] private UnityEvent onThrown;

        private bool _isKinematic;
        private Transform _originalParent;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _originalParent = _transform.parent;
            _isKinematic = rigidbody.isKinematic;
        }

        public void Grab(Transform parent)
        {
            _transform.position = parent.position;
            _transform.parent = parent;
            rigidbody.isKinematic = true;
            onGrabbed?.Invoke();
        }

        public void Drop()
        {
            _transform.parent = _originalParent;
            rigidbody.isKinematic = _isKinematic;
            onDropped?.Invoke();
        }

        public void Throw(Vector3 velocity)
        {
            _transform.parent = _originalParent;
            rigidbody.isKinematic = _isKinematic;
            rigidbody.velocity = velocity;
            onThrown?.Invoke();
        }
    }
}