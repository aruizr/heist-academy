using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities
{
    public abstract class PropertyTweener<T> : MonoBehaviour
    {
        [SerializeField] protected T offset;
        [SerializeField] protected float duration;
        [SerializeField] private Ease ease;
        [SerializeField] [Min(-1)] private int loops = 1;
        [SerializeField] private LoopType loopType;
        [SerializeField] private bool autoPlay;
        [SerializeField] private UnityEvent onComplete;

        private bool _forward;

        private void OnEnable()
        {
            if (autoPlay) PlayForward();
        }

        private void ConfigTween(Tweener tweener)
        {
            tweener.SetEase(ease).SetLoops(loops, loopType).OnStepComplete(() => onComplete?.Invoke()).SetLink(gameObject, LinkBehaviour.PauseOnDisable);
        }

        public void PlayForward()
        {
            ConfigTween(GetForwardTween());
            _forward = true;
        }

        protected abstract Tweener GetForwardTween();

        public void PlayBackward()
        {
            ConfigTween(GetBackwardTween());
            _forward = false;
        }

        protected abstract Tweener GetBackwardTween();

        public void TogglePlayForwardBackward()
        {
            if (_forward)
                PlayBackward();
            else
                PlayForward();
        }
    }
}