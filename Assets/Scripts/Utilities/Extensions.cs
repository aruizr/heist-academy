using UnityEngine;
using UnityEngine.AI;

namespace Utilities
{
    public static class Extensions
    {
        public static bool HasReachedDestination(this NavMeshAgent agent)
        {
            if (agent.pathPending) return false;
            if (!(agent.remainingDistance <= agent.stoppingDistance)) return false;
            return !agent.hasPath || agent.velocity.sqrMagnitude == 0f;
        }

        public static bool CanReachDestination(this NavMeshAgent agent)
        {
            return agent.pathStatus == NavMeshPathStatus.PathComplete;
        }

        public static Vector3 Rotate(this Vector3 vector, float angle, Vector3 axis)
        {
            return Quaternion.AngleAxis(angle, axis) * vector;
        }
    }
}