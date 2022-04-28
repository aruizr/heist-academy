using System;
using System.Collections.Generic;
using System.Linq;
using Codetox.Core;
using Codetox.Variables;
using DG.Tweening;
using Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Cam
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private Vector3Variable positionOffset;

        [SerializeField] private Vector3Variable rotationOffset;
        [SerializeField] private CameraTypeEnumVariable cameraType;
        [SerializeField] private FloatVariable rotationSpeed;
        [SerializeField] private FloatVariable anglesPerStep;

        [Header("Player")] [SerializeField] private Vector3Variable currentPlayerPosition;

        [SerializeField] private FloatVariable distanceToPlayer;

        private new Camera camera;
        private Transform cameraTransform;
        private Vector3 currentEulerAngles;
        private Vector3 deltaEulerAngles;

        private void Start()
        {
            camera = Camera.main;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if(camera != null)
                cameraTransform = camera.transform;
        }

        private void LateUpdate()
        {
            if (cameraType.Value == CameraType.Continuous)
            {
                currentEulerAngles += deltaEulerAngles;
                cameraTransform.rotation = Quaternion.Euler(currentEulerAngles + rotationOffset.Value);
            }

            cameraTransform.position = GetFinalPositionValue() - GetFinalDistanceValue();
        }

        public void MoveCamera(Vector2 direction)
        {
            if (cameraType.Value == CameraType.Continuous)
            {
                deltaEulerAngles = new Vector3(0f, direction.x, 0f) * rotationSpeed.Value * 0.1f;
                return;
            }

            if (direction.x > 0)
            {
                currentEulerAngles.y += anglesPerStep.Value;
                cameraTransform.DORotate(currentEulerAngles + rotationOffset.Value, 0.5f);
            }
            else if (direction.x < 0)
            {
                currentEulerAngles.y -= anglesPerStep.Value;
                cameraTransform.DORotate(currentEulerAngles + rotationOffset.Value, 0.5f);
            }
        }

        private Vector3 GetFinalPositionValue()
        {
            return currentPlayerPosition.Value + positionOffset.Value;
        }

        private Vector3 GetFinalDistanceValue()
        {
            return cameraTransform.transform.forward * distanceToPlayer.Value;
        }
    }
}