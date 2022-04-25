using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private FloatVariable movementSpeed;
        [SerializeField] private FloatVariable smoothTime;
        [SerializeField] private Vector3Variable currentVelocity;
        [SerializeField] private Vector2Variable currentDirection;

        private Vector2 _currentVelocity;

        private void Update()
        {
            var currentHorizontalVelocity = GetCurrentHorizontalVelocity();
            var targetHorizontalVelocity = GetTargetHorizontalVelocity();
            var finalHorizontalVelocity =
                GetFinalHorizontalVelocity(currentHorizontalVelocity, targetHorizontalVelocity);
            var finalVelocity = GetFinalVelocity(finalHorizontalVelocity);

            currentVelocity.Value = finalVelocity;
            controller.Move(finalVelocity * Time.deltaTime);
        }

        private Vector3 GetFinalVelocity(Vector2 finalHorizontalVelocity)
        {
            return new Vector3(finalHorizontalVelocity.x, currentVelocity.Value.y, finalHorizontalVelocity.y);
        }

        private Vector2 GetFinalHorizontalVelocity(Vector2 currentHorizontalVelocity, Vector2 targetHorizontalVelocity)
        {
            return Vector2.SmoothDamp(currentHorizontalVelocity, targetHorizontalVelocity, ref _currentVelocity,
                smoothTime.Value);
        }

        private Vector2 GetTargetHorizontalVelocity()
        {
            return currentDirection.Value * movementSpeed.Value;
        }

        private Vector2 GetCurrentHorizontalVelocity()
        {
            return new Vector2(currentVelocity.Value.x, currentVelocity.Value.z);
        }
    }
}