using Codetox.Core;
using Codetox.Messaging;
using Interactions;
using UnityEngine;
using UnityEngine.AI;
using Variables;

namespace AI.Guard
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ValueReference<float> closingTime;

        private Door _current;

        public void OpenDoor(GameObject target)
        {
            target.Send<Door>(OpenDoor, MessageScope.Parents);
        }

        public void CloseDoor(GameObject target)
        {
            target.Send<Door>(CloseDoor, MessageScope.Parents);
        }

        private void CloseDoor(Door door)
        {
            if (!door.IsOpen) return;
            this.Coroutine().WaitForSeconds(closingTime.Value).Invoke(door.Close).Run();
        }

        public void OpenDoor(Door door)
        {
            if (door.IsOpen) return;
            _current = door;
            navMeshAgent.isStopped = true;
            _current.ForceOpen();
            _current.onFinishedOpening.AddListener(OnDoorFinishedOpening);
        }

        private void OnDoorFinishedOpening()
        {
            navMeshAgent.isStopped = false;
            _current.onFinishedOpening.RemoveListener(OnDoorFinishedOpening);
            _current = null;
        }
    }
}