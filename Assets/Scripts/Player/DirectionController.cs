using Codetox.Variables;
using UnityEngine;

namespace Player
{
    public class DirectionController : MonoBehaviour
    {
        [SerializeField] private Vector2Variable currentDirection;

        private Transform _cameraTransform;
        private Vector2 _direction;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (_direction.magnitude < 0.1f)
            {
                currentDirection.Value = Vector2.zero;
                return;
            }

            var angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            var finalDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            currentDirection.Value = new Vector2(finalDirection.x, finalDirection.z);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}