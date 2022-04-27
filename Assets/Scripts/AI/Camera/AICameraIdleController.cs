using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace AI.Camera
{
    public class AICameraIdleController: MonoBehaviour
    {
        [Serializable]
        public struct SurveillanceStep
        {
            public Vector3 rotation;
            public float time;
        }

        [SerializeField] private Transform controlTransform;
        [SerializeField] private SurveillanceStep[] surveillanceSteps;
        [SerializeField] private ValueReference<float> rotationSpeed;
        
        public UnityEvent onStepSequenceComplete;
        
        private int _index;
        
        private void OnEnable()
        {
            StartCoroutine(DoSurveillance());
        }

        private void OnDisable()
        {
            StopCoroutine(DoSurveillance());
        }

        private IEnumerator DoSurveillance()
        {
            while (true)
            {
                if (surveillanceSteps.Length == 0) yield return new WaitUntil(() => surveillanceSteps.Length > 0);
                var step = surveillanceSteps[_index];
                var isComplete = false;
                var angle = Quaternion.Angle(controlTransform.rotation, Quaternion.Euler(step.rotation));
                controlTransform.DOLocalRotate(step.rotation, angle / rotationSpeed.Value).SetEase(Ease.Linear).OnComplete(() => isComplete = true).SetLink(gameObject, LinkBehaviour.KillOnDisable);
                yield return new WaitUntil(() => isComplete);
                yield return new WaitForSeconds(step.time);
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
    }
}