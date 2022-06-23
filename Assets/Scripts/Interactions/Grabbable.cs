using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Interactions
{
    public class Grabbable : Selectable, IGrabbable, IThrowable
    {
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private ValueReference<Vector3> rotationOffset;

        public UnityEvent onGrabbed;
        public UnityEvent onDropped;
        public UnityEvent onThrown;

        private Transform _toFollow;

        private void FixedUpdate()
        {
            if (!_toFollow) return;
            rigidbody.MovePosition(_toFollow.position);
            rigidbody.MoveRotation(Quaternion.Euler(_toFollow.eulerAngles + rotationOffset.Value));
        }

        public bool IsGrabbed { get; private set; }

        public void ToggleGrabDrop(Transform parent)
        {
            if (IsGrabbed) Drop();
            else Grab(parent);
        }

        public void Throw(Vector3 velocity)
        {
            if (!IsGrabbed) return;
            _toFollow = null;
            rigidbody.isKinematic = false;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidbody.velocity = velocity;
            IsGrabbed = false;
            onThrown?.Invoke();
        }

        private void Grab(Transform parent)
        {
            if (IsGrabbed) return;
            _toFollow = parent;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rigidbody.isKinematic = true;
            IsGrabbed = true;
            onGrabbed?.Invoke();
        }

        private void Drop()
        {
            if (!IsGrabbed) return;
            _toFollow = null;
            rigidbody.isKinematic = false;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            IsGrabbed = false;
            onDropped?.Invoke();
        }

        public override void Unselect()
        {
            Drop();
            base.Unselect();
        }
    }
}