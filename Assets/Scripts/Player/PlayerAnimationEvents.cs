using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public UnityEvent onWalkingStep;
        public UnityEvent onCrouchingStep;

        public void WalkingStep()
        {
            onWalkingStep?.Invoke();
        }

        public void CrouchingStep()
        {
            onCrouchingStep?.Invoke();
        }
    }
}