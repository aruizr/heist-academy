using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace AI.Camera
{
    public class SurveillanceController : MonoBehaviour
    {
        [SerializeField] private Transform controlTransform;
        [SerializeField] private SurveillanceStep[] surveillanceSteps;
        [SerializeField] private ValueReference<float> rotationSpeed;
        [SerializeField] private Ease rotationEase;

        public UnityEvent onStepSequenceComplete;
        private Tweener _currentTween;

        private int _index;

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

                var step = surveillanceSteps[_index];
                var rotation = Quaternion.Euler(step.rotation.Value);
                var angle = Quaternion.Angle(controlTransform.rotation, rotation);
                var time = angle / rotationSpeed.Value;
                var isComplete = false;

                _currentTween?.Kill();
                _currentTween = controlTransform.DOLocalRotate(step.rotation.Value, time).SetEase(rotationEase)
                    .OnComplete(() => isComplete = true);

                yield return new WaitUntil(() => isComplete);
                yield return new WaitForSeconds(step.time.Value);

                IncrementIndex();
            }
        }

        private void IncrementIndex()
        {
            _index++;
            if (_index < surveillanceSteps.Length) return;
            _index = 0;
            onStepSequenceComplete.Invoke();
        }

        [Serializable]
        public struct SurveillanceStep
        {
            public ValueReference<Vector3> rotation;
            public ValueReference<float> time;
        }
    }
}