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

        public void OpenDoor(GameObject target)
        {
            target.Send<Door>(door =>
            {
                if (door.IsOpen) return;
                _current = door;
                navMeshAgent.isStopped = true;
                _current.Open();
                _current.onFinishOpening.AddListener(OnDoorFinishedOpening);
            }, MessageScope.Parents);
        }

        private void OnDoorFinishedOpening()
        {
            navMeshAgent.isStopped = false;
            _current.onFinishOpening.RemoveListener(OnDoorFinishedOpening);
            _current = null;
        }
    }
}