using DG.Tweening;
using UnityEngine;

namespace PropertyAnimators
{
    public class TransformScaleAnimator : PropertyAnimator<Transform, Vector3>
    {
        private Vector3 _original;

        private void Awake()
        {
            _original = transform.localScale;
        }

        protected override Tweener GetForwardTween()
        {
            return target.DOScale(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return target.DOScale(_original, duration);
        }
    }
}