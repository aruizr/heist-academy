using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Utilities;
using Variables;

namespace AI.Camera
{
    public class SurveillanceController : MonoBehaviour
    {
        [SerializeField] private Transform controlTransform;
        [SerializeField] private ValueReference<float> rotationSpeed;
        [SerializeField] private Ease rotationEase;
        [SerializeField] private SurveillanceStep[] surveillanceSteps;

        private Tweener _currentTween;

        private void OnEnable()
        {
            StartCoroutine(DoSurveillance());
        }

        private void OnDisable()
        {
            StopCoroutine(DoSurveillance());
            _currentTween?.Kill();
        }

        private IEnumerator DoSurveillance()
        {
            if (surveillanceSteps.Length == 0) yield return new WaitUntil(() => surveillanceSteps.Length > 0);
            foreach (var step in surveillanceSteps.WrappedAround())
            {
                var stepRotation = Quaternion.Euler(step.rotation);
                var angleToRotation = Quaternion.Angle(controlTransform.rotation, stepRotation);
                var timeToReachRotation = angleToRotation / rotationSpeed.Value;
                var isRotationComplete = false;

                _currentTween?.Kill();
                _currentTween = controlTransform.
                    DOLocalRotate(step.rotation, timeToReachRotation).
                    SetEase(rotationEase).
                    OnComplete(() => isRotationComplete = true);

                yield return new WaitUntil(() => isRotationComplete);
                yield return new WaitForSeconds(step.time);
            }
        }

        [Serializable]
        public struct SurveillanceStep
        {
            public Vector3 rotation;
            public float time;
        }
    }
}