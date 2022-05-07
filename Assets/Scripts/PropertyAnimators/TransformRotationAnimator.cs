using DG.Tweening;
using UnityEngine;

namespace PropertyAnimators
{
    public class TransformRotationAnimator : PropertyAnimator<Transform, Vector3>
    {
        private Vector3 _original;

        private void Awake()
        {
            _original = transform.eulerAngles;
        }

        protected override Tweener GetForwardTween()
        {
            return target.DORotate(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return target.DORotate(_original, duration);
        }
    }
}