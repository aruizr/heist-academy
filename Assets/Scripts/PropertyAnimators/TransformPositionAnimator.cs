using DG.Tweening;
using UnityEngine;

namespace PropertyAnimators
{
    public class TransformPositionAnimator : PropertyAnimator<Transform, Vector3>
    {
        private Vector3 _original;

        private void Awake()
        {
            _original = transform.position;
        }

        protected override Tweener GetForwardTween()
        {
            return target.DOMove(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return target.DOMove(_original, duration);
        }
    }
}