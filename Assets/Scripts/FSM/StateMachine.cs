using System.Linq;
using Codetox.Attributes;
using UnityEngine;

namespace FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State initialState;
        [Disabled] [SerializeField] private State currentState;
        [SerializeField] private State[] states;

        private void OnEnable()
        {
            foreach (var state in states.Where(state => !state.Equals(initialState))) state.gameObject.SetActive(false);
            SetState(initialState);
        }

        private void OnDisable()
        {
            if (currentState) currentState.Exit();
        }

        public void SetState(State state)
        {
            if (currentState) currentState.Exit();
            currentState = state;
            if (currentState) currentState.Enter();
        }
    }
}