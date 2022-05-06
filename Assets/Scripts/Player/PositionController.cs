using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class PositionController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3Variable currentPosition;

        private void Update()
        {
            currentPosition.Value = target.position;
        }
    }
}