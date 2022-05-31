using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace PropertyAnimators
{
    public abstract class PropertyAnimator<T, TV> : MonoBehaviour
    {
        [SerializeField] protected T target;
        [SerializeField] protected TV offset;
        [SerializeField] protected float duration;
        [SerializeField] protected bool useUnscaledTime;
        [SerializeField] private Ease ease;
        [SerializeField] [Min(-1)] private int loops = 1;
        [SerializeField] private LoopType loopType;
        [SerializeField] private bool autoPlay;

        public UnityEvent onComplete;

        private bool _forward;
        private Tweener _tweener;

        private void OnEnable()
        {
            if (autoPlay) PlayForward();
        }

        private void ConfigTween(Tweener tweener)
        {
            _tweener?.Kill();
            _tweener = tweener.
                SetEase(ease).
                SetLoops(loops, loopType).
                OnStepComplete(() => onComplete?.Invoke()).
                SetLink(gameObject, LinkBehaviour.PauseOnDisable).
                SetUpdate(useUnscaledTime);
        }

        public void PlayForward()
        {
            ConfigTween(GetForwardTween());
            _forward = true;
        }

        public void PlayBackward()
        {
            ConfigTween(GetBackwardTween());
            _forward = false;
        }

        public void TogglePlayForwardBackward()
        {
            if (_forward)
                PlayBackward();
            else
                PlayForward();
        }

        protected abstract Tweener GetForwardTween();
        protected abstract Tweener GetBackwardTween();
    }
}