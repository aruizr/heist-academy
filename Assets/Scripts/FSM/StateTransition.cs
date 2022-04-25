using UnityEngine;

namespace FSM
{
    public abstract class StateTransition : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private State endState;

        private void Update()
        {
            if (Evaluate()) stateMachine.SetState(endState);
        }

        protected abstract bool Evaluate();
    }
}