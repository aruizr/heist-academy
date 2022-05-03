using Interactions.Final;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class Grabbeable : Selectible, IGrabbeable, IInteractable
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private UnityEvent onGrabbed;
        [SerializeField] private UnityEvent onDropped;
        [SerializeField] private UnityEvent onThrown;

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

        public void Grab(Transform parent)
        {
            _toFollow = parent;
            rigidbody.isKinematic = true;
            onGrabbed?.Invoke();
        }

        public void Drop()
        {
            _toFollow = null;
            rigidbody.isKinematic = _isKinematic;
            onDropped?.Invoke();
        }

        public void Throw(Vector3 velocity)
        {
            _toFollow = null;
            rigidbody.isKinematic = _isKinematic;
            rigidbody.velocity += velocity;
            onThrown?.Invoke();
        }

        public void Interact()
        {
        }
    }
}