using UnityEngine;
using Variables;

namespace Cam
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private ValueReference<Vector3> positionOffset;
        [SerializeField] private ValueReference<Vector3> rotationOffset;
        [SerializeField] private ValueReference<float> rotationSpeed;
        [SerializeField] private ValueReference<float> rotationSmoothTime;
        [SerializeField] private ValueReference<float> distanceToPlayer;
        [SerializeField] private ValueReference<Vector3> currentPlayerPosition;
        [SerializeField] private ValueReference<float> minVerticalRotation;
        [SerializeField] private ValueReference<float> maxVerticalRotation;

        private Transform _cameraTransform;
        private Vector2 _currentDirection;
        private Vector3 _currentRotation;
        private Vector3 _currentVelocity;
        private Vector3 _targetRotation;

        private void Awake()
        {
            if (Camera.main) _cameraTransform = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            // _targetRotation += new Vector3(0f, _currentDirection.x, 0f) * rotationSpeed.Value * Time.fixedDeltaTime;
            // _currentRotation = Vector3.SmoothDamp(_currentRotation, _targetRotation, ref _currentVelocity,
            //     rotationSmoothTime.Value);

            _currentRotation += new Vector3(_currentDirection.y, _currentDirection.x, 0f) * rotationSpeed.Value * Time.fixedDeltaTime;
            _currentRotation.x = Mathf.Clamp(_currentRotation.x, minVerticalRotation.Value, maxVerticalRotation.Value);
        }

        private void LateUpdate()
        {
            var targetRotation = Quaternion.Euler(_currentRotation);
            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, targetRotation, rotationSmoothTime.Value);
            _cameraTransform.position = currentPlayerPosition.Value + positionOffset.Value -
                                        _cameraTransform.forward * distanceToPlayer.Value;
        }

        public void MoveCamera(Vector2 direction)
        {
            _currentDirection = direction;
        }
    }
}