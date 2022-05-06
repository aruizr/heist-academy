using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private FloatVariable maxJumpHeight;
        [SerializeField] private FloatVariable minJumpHeight;
        [SerializeField] private FloatVariable jumpApexTime;
        [SerializeField] private Vector3Variable currentVelocity;

        private bool _isJumping;

        private void Update()
        {
            var gravity = GetGravity();
            var maxJumpVelocity = GetJumpVelocity(maxJumpHeight.Value);
            var minJumpVelocity = GetJumpVelocity(minJumpHeight.Value);
            var verticalVelocity = GetVerticalVelocity(maxJumpVelocity, minJumpVelocity, gravity);
            var finalVelocity = GetFinalVelocity(verticalVelocity);

            currentVelocity.Value = finalVelocity;
            controller.Move(finalVelocity * Time.deltaTime);
        }

        private Vector3 GetFinalVelocity(float verticalVelocity)
        {
            var currentPlayerVelocity = currentVelocity.Value;
            return new Vector3(currentPlayerVelocity.x, verticalVelocity, currentPlayerVelocity.z);
        }

        private float GetVerticalVelocity(float maxJumpVelocity, float minJumpVelocity, float gravity)
        {
            var currentVerticalVelocity = currentVelocity.Value.y;
            return controller.isGrounded switch
            {
                true when _isJumping => maxJumpVelocity,
                true => 0f,
                false when !_isJumping && currentVerticalVelocity > minJumpVelocity => minJumpVelocity,
                false => currentVerticalVelocity - gravity * Time.deltaTime
            };
        }

        private float GetGravity()
        {
            return 2 * maxJumpHeight.Value / (jumpApexTime.Value * jumpApexTime.Value);
        }

        private float GetJumpVelocity(float jumpHeight)
        {
            return 2 * jumpHeight / jumpApexTime.Value;
        }

        public void StartJumping()
        {
            _isJumping = true;
        }

        public void StopJumping()
        {
            _isJumping = false;
        }
    }
}