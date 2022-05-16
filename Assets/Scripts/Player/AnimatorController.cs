using Codetox.Variables;
using UnityEngine;
using Variables;

namespace Player
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Vector2Variable direction;
        [SerializeField] private ValueReference<string> velocityParameter;
        [SerializeField] private ValueReference<float> smoothTime;

        private void FixedUpdate()
        {
            animator.SetFloat(velocityParameter.Value, direction.Value.magnitude, smoothTime.Value, Time.fixedDeltaTime);
        }
    }
}