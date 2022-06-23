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
            target.Send<BasicDoor>(OpenDoor, MessageScope.Parents);
        }

        public void CloseDoor(GameObject target)
        {
            target.Send<BasicDoor>(CloseDoor, MessageScope.Parents);
        }
        
        public void OpenDoor(BasicDoor door)
        {
            if (door.IsOpen) return;
            navMeshAgent.isStopped = true;
            door.ForceOpen(navMeshAgent.transform);
            door.onFinishedOpening.AddListener(() => OnDoorFinishedOpening(door));
        }

        private void CloseDoor(BasicDoor door)
        {
            door.gameObject.
                Coroutine().
                WaitForSeconds(closeDoorDelay.Value).
                Invoke(() => { if (door.IsOpen) door.Close(); }).
                Run();
        }

        private void OnDoorFinishedOpening(BasicDoor door)
        {
            navMeshAgent.isStopped = false;
            door.onFinishedOpening.RemoveListener(() => OnDoorFinishedOpening(door));
        }
    }
}