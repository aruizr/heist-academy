using UnityEngine;

namespace FSM
{
    public class StateHolder : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;

        public State CurrentState { get; set; }

        public void ChangeToCurrentState()
        {
            stateMachine.SetState(CurrentState);
        }
    }
}