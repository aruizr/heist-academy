using System;
using System.Collections.Generic;
using System.Linq;
using Codetox.Core;
using Codetox.Variables;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Variables;
using FloatVariable = Codetox.Variables.FloatVariable;
using IntVariable = Codetox.Variables.IntVariable;

namespace Cam
{
    public class WallDetection : MonoBehaviour
    {
        [Header("Camera")] [SerializeField] private Vector3Variable positionOffset;
    
        [Header("Player")] [SerializeField] private Vector3Variable currentPlayerPosition;
        [SerializeField] private FloatVariable distanceToPlayer;

        [Header("Transparent Sphere")] 
        [SerializeField] private GameObject sphereGameObject;
        [SerializeField] private IntVariable sphereMaxSize;
        
        [Header("Environment")] 
        [SerializeField] private LayerMaskEnumVariable visibleLayerOnRayHit;
    
        private new Camera camera;
        private Transform cameraTransform;
        private Ray ray;
    
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
            sphereGameObject.transform.LookAt(cameraTransform);
            var sphereRotation = sphereGameObject.transform.localEulerAngles;
            sphereRotation.x += 90;
            sphereGameObject.transform.localEulerAngles = sphereRotation;
            
            var position = cameraTransform.position;
            //cameraTransform.DirectionTo(currentPlayerPosition.Value);
            Vector3 screenPos = camera.WorldToScreenPoint(currentPlayerPosition.Value);
            ray = camera.ScreenPointToRay(screenPos);

            Vector3 rayEndPoint = GetFinalPositionValue();
            Debug.DrawLine(position, rayEndPoint, Color.blue);

            RaycastHit hit;
            
            if(Physics.Raycast (ray, out hit, distanceToPlayer.Value))
            {
                if (hit.collider.gameObject.IsInLayerMask(visibleLayerOnRayHit.Value))
                {
                    sphereGameObject.transform.DOScale(sphereMaxSize.Value, 0.5f);
                }
                else
                {
                    sphereGameObject.transform.DOScale(0f, 0.5f);
                }
            }
            
        }
    
        private Vector3 GetFinalPositionValue()
        {
            return currentPlayerPosition.Value + positionOffset.Value;
        }
    }
}
