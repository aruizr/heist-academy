using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class RotationTweener : PropertyTweener<Vector3>
    {
        private Vector3 _original;

        private void Awake()
        {
            _original = transform.eulerAngles;
        }

        protected override Tweener GetForwardTween()
        {
            return transform.DORotate(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return transform.DORotate(_original, duration);
        }
    }
}