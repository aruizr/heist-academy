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

        private Transform _cameraTransform;
        private Vector3 _currentRotation;
        private Vector3 _currentVelocity;
        private Vector3 _deltaRotation;

        private void Awake()
        {
            if (Camera.main) _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            _currentRotation = GetCurrentRotation();
            _cameraTransform.rotation = Quaternion.Euler(_currentRotation + rotationOffset.Value);
            _cameraTransform.position = GetFinalPositionValue() - GetFinalDistanceValue();
        }
        
        public void MoveCamera(Vector2 direction)
        {
            _deltaRotation = new Vector3(0f, direction.x, 0f) * rotationSpeed.Value;
        }

        private Vector3 GetCurrentRotation()
        {
            return Vector3.SmoothDamp(
                _currentRotation,
                _currentRotation + _deltaRotation,
                ref _currentVelocity,
                rotationSmoothTime.Value);
        }

        private Vector3 GetFinalPositionValue()
        {
            return currentPlayerPosition.Value + positionOffset.Value;
        }

        private Vector3 GetFinalDistanceValue()
        {
            return _cameraTransform.transform.forward * distanceToPlayer.Value;
        }
    }
}