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
        [SerializeField] private ValueReference<float> closeDoorDelay;

        public void OpenDoor(GameObject target)
        {
            target.Send<Door>(OpenDoor, MessageScope.Parents);
        }

        public void CloseDoor(GameObject target)
        {
            target.Send<Door>(CloseDoor, MessageScope.Parents);
        }
        
        public void OpenDoor(Door door)
        {
            if (door.IsOpen) return;
            navMeshAgent.isStopped = true;
            door.ForceOpen();
            door.onFinishedOpening.AddListener(() => OnDoorFinishedOpening(door));
        }

        private void CloseDoor(Door door)
        {
            door.gameObject.
                Coroutine().
                WaitForSeconds(closeDoorDelay.Value).
                Invoke(() => { if (door.IsOpen) door.Close(); }).
                Run();
        }

        private void OnDoorFinishedOpening(Door door)
        {
            navMeshAgent.isStopped = false;
            door.onFinishedOpening.RemoveListener(() => OnDoorFinishedOpening(door));
        }
    }
}