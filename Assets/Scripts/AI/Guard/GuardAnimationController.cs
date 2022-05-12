using UnityEngine;
using UnityEngine.AI;
using Variables;

namespace AI.Guard
{
    public class GuardAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<string> parameterName;

        private void FixedUpdate()
        {
            animator.SetFloat(parameterName.Value, navMeshAgent.velocity.magnitude);
        }
    }
}