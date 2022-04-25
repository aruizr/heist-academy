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

        [Header("Environment")] 
        [SerializeField] private LayerMaskEnumVariable visibleLayerOnRayHit;
        [SerializeField] private UnityEvent<GameObject> startEvent;
        [SerializeField] private UnityEvent<GameObject> endEvent;
        
        private new Camera camera;
        private Transform cameraTransform;
        private Vector3 currentEulerAngles;
        private Vector3 deltaEulerAngles;
        private Ray ray;
        private List<GameObject> objectsThatAreInvisible = new List<GameObject>();
        private List<GameObject> objectsToMakeInvisible;

        private void Start()
        {
            camera = Camera.main;
            if(camera != null)
                cameraTransform = camera.transform;
        }

        private void Update()
        {
            var position = cameraTransform.position;
            //cameraTransform.DirectionTo(currentPlayerPosition.Value);
            Vector3 screenPos = camera.WorldToScreenPoint(currentPlayerPosition.Value);
            ray = camera.ScreenPointToRay(screenPos);

            Vector3 rayEndPoint = GetFinalPositionValue();
            Debug.DrawLine(position, rayEndPoint, Color.blue);

            ObjectDetectionManagement(Physics.RaycastAll(ray, distanceToPlayer.Value));
            
        }

        private void ObjectDetectionManagement(RaycastHit[] objectsHit)
        {
            var gameObjectsHit = new List<GameObject>();
            try
            {
                if (objectsHit == null) { throw new Exception("OBJECTS HIT IS NULL"); }
                
                objectsToMakeInvisible = new List<GameObject>();
                foreach (var raycastHit in objectsHit)
                {
                    var inspectedObject = raycastHit.transform.gameObject;
                    gameObjectsHit.Add(inspectedObject);
                    if (!inspectedObject.IsInLayerMask(visibleLayerOnRayHit.Value)) continue;
                    if (objectsThatAreInvisible.Contains(inspectedObject)) continue;
                    objectsThatAreInvisible.Add(inspectedObject);
                    objectsToMakeInvisible.Add(inspectedObject);
                }
            }
            catch (Exception ex)
            {
                // Debug.Log("CAMERA CONTROLLER > UPDATE > OBJECT DETECTION MANAGEMENT > ERROR");
                // Debug.Log(ex);
            }
            DetectObjectsOut(objectsThatAreInvisible.Where(obj => !gameObjectsHit.Contains(obj)).ToList());
        }

        private void DetectObjectsOut(IEnumerable<GameObject> objectsToEliminate)
        {
            try
            {
                foreach (var o in objectsToEliminate)
                {
                    objectsThatAreInvisible.Remove(o);
                    endEvent?.Invoke(o);
                }
            }
            catch (Exception ex)
            {
                // Debug.Log("CAMERA CONTROLLER > UPDATE > OBJECT DETECTION MANAGEMENT > DETECT OBJECTS OUT > ERROR");
                // Debug.Log(ex);
            }
        }

        private void LateUpdate()
        {
            if (cameraType.Value == CameraType.Continuous)
            {
                currentEulerAngles += deltaEulerAngles;
                cameraTransform.rotation = Quaternion.Euler(currentEulerAngles + rotationOffset.Value);
            }

            cameraTransform.position = GetFinalPositionValue() - GetFinalDistanceValue();

            ManageObjectsToMakeInvisible();
        }

        private void ManageObjectsToMakeInvisible()
        {
            try
            {
                foreach (var o in objectsToMakeInvisible)
                {
                    startEvent?.Invoke(o);
                }
            }
            catch (Exception ex)
            {
                // Debug.Log("CAMERA CONTROLLER > LATE UPDATE > MANAGE OBJECTS TO MAKE INVISIBLE > ERROR");
                // Debug.Log(ex);
            }
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