using Codetox.Messaging;
using Interactions;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Guard
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;

        private Door _current;

        public void EvaluateDoor(GameObject target)
        {
            target.Send<Door>(EvaluateDoor, MessageScope.Parents);
        }

        public void EvaluateDoor(Door door)
        {
            if (!door.IsOpen) Open(door);
        }

        private void Open(Door door)
        {
            _current = door;
            navMeshAgent.isStopped = true;
            _current.Open();
            _current.onFinishOpening.AddListener(OnDoorFinishedOpening);
        }

        private void OnDoorFinishedOpening()
        {
            navMeshAgent.isStopped = false;
            _current.onFinishOpening.RemoveListener(OnDoorFinishedOpening);
            _current = null;
        }
    }
}