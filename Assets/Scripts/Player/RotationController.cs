using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private FloatVariable rotationSpeed;
        [SerializeField] private Vector2Variable currentDirection;
        [SerializeField] private Vector3Variable currentRotation;

        private Vector3 _forward;

        private void Update()
        {
            var direction = currentDirection.Value;

            if (direction != Vector2.zero) _forward = new Vector3(direction.x, target.forward.y, direction.y);

            var targetRotation = Quaternion.LookRotation(_forward);
            target.rotation = CurrentRotation(targetRotation);
            currentRotation.Value = target.eulerAngles;
        }

        private void OnEnable()
        {
            target.eulerAngles = currentRotation.Value;
            _forward = target.forward;
        }

        private Quaternion CurrentRotation(Quaternion targetRotation)
        {
            return Quaternion.Slerp(target.rotation, targetRotation, rotationSpeed.Value * Time.deltaTime);
        }
    }
}