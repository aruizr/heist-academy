using UnityEngine;
using UnityEngine.Events;

namespace FSM
{
    public class State : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnterState;
        [SerializeField] private UnityEvent onExitState;

        public void Enter()
        {
            onEnterState?.Invoke();
            gameObject.SetActive(true);
        }

        public void Exit()
        {
            onExitState?.Invoke();
            gameObject.SetActive(false);
        }
    }
}