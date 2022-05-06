using System;
using System.Collections.Generic;
using System.Linq;
using Codetox.Core;
using Codetox.Variables;
using UnityEngine;
using Variables;

namespace Cam
{
    public class WallDetection : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private Vector3Variable positionOffset;
    
        [Header("Player")] [SerializeField] private Vector3Variable currentPlayerPosition;
        [SerializeField] private FloatVariable distanceToPlayer;
    
        [Header("Environment")] 
        [SerializeField] private LayerMaskVariable visibleLayerOnRayHit;
        [SerializeField] private FloatVariable wallMaterialAlpha;
    
        private new Camera camera;
        private Transform cameraTransform;
        private Ray ray;
    
        private readonly List<GameObject> objectsThatAreInvisible = new List<GameObject>();
        private List<GameObject> objectsToMakeInvisible;
        // Start is called before the first frame update
        private void Start()
        {
            camera = Camera.main;
            if(camera != null)
                cameraTransform = camera.transform;
        }

        // Update is called once per frame
        private void Update()
        {
            var position = cameraTransform.position;
            //cameraTransform.DirectionTo(currentPlayerPosition.Value);
            Vector3 screenPos = camera.WorldToScreenPoint(currentPlayerPosition.Value);
            ray = camera.ScreenPointToRay(screenPos);

            Vector3 rayEndPoint = GetFinalPositionValue();
            Debug.DrawLine(position, rayEndPoint, Color.blue);

            // ReSharper disable once Unity.PreferNonAllocApi
            ObjectDetectionManagement(Physics.RaycastAll(ray, distanceToPlayer.Value));
        }
    
        private void ObjectDetectionManagement(RaycastHit[] objectsHit)
        {
            var gameObjectsHit = new List<GameObject>();
                
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
            DetectObjectsOut(objectsThatAreInvisible.Where(obj => !gameObjectsHit.Contains(obj)).ToList());
        }
    
        private void DetectObjectsOut(IEnumerable<GameObject> objectsToEliminate)
        {
            foreach (var o in objectsToEliminate)
            {
                objectsThatAreInvisible.Remove(o);
                Color color = o.GetComponent<Renderer>().material.color;
                color.a += wallMaterialAlpha.Value;
                o.GetComponent<Renderer>().material.color = color;
            }
        }

        private void LateUpdate()
        {
            ManageObjectsToMakeInvisible();
        }

        private void ManageObjectsToMakeInvisible()
        {
            foreach (var o in objectsToMakeInvisible)
            {
                Color color = o.GetComponent<Renderer>().material.color;
                color.a -= wallMaterialAlpha.Value;
                o.GetComponent<Renderer>().material.color = color;
            }
        }
    
        private Vector3 GetFinalPositionValue()
        {
            return currentPlayerPosition.Value + positionOffset.Value;
        }
    }
}
