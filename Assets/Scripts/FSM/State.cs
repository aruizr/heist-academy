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
            gameObject.SetActive(true);
            onEnterState?.Invoke();
        }

        public void Exit()
        {
            gameObject.SetActive(false);
            onExitState?.Invoke();
        }
    }
}