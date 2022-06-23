using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public UnityEvent onWalkingStep;
        public UnityEvent onCrouchingStep;
        public UnityEvent onFinishedThrowing;
        public UnityEvent onThrow;

        public void WalkingStep()
        {
            onWalkingStep?.Invoke();
        }

        public void CrouchingStep()
        {
            onCrouchingStep?.Invoke();
        }

        public void FinishedThrowing()
        {
            onFinishedThrowing?.Invoke();
        }

        public void Throw()
        {
            onThrow?.Invoke();
        }
    }
}