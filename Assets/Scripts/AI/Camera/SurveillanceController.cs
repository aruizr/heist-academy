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
        private IEnumerator<SurveillanceStep> _enumerator;

        private void Awake()
        {
            _enumerator = surveillanceSteps.Cast<SurveillanceStep>().GetEnumerator();
        }

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
            while (true)
            {
                if (surveillanceSteps.Length == 0) yield return new WaitUntil(() => surveillanceSteps.Length > 0);

                var currentStep = _enumerator.GetWrapAroundNext();
                var stepRotation = Quaternion.Euler(currentStep.rotation.Value);
                var angleToRotation = Quaternion.Angle(controlTransform.rotation, stepRotation);
                var timeToReachRotation = angleToRotation / rotationSpeed.Value;
                var isRotationComplete = false;

                _currentTween?.Kill();
                _currentTween = controlTransform.
                    DOLocalRotate(currentStep.rotation.Value, timeToReachRotation).
                    SetEase(rotationEase).
                    OnComplete(() => isRotationComplete = true);

                yield return new WaitUntil(() => isRotationComplete);
                yield return new WaitForSeconds(currentStep.time.Value);
            }
        }

        [Serializable]
        public struct SurveillanceStep
        {
            public ValueReference<Vector3> rotation;
            public ValueReference<float> time;
        }
    }
}