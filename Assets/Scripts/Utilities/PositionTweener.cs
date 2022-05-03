using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class PositionTweener : PropertyTweener<Transform, Vector3>
    {
        private Vector3 _original;

        private void Awake()
        {
            _original = transform.position;
        }

        protected override Tweener GetForwardTween()
        {
            return transform.DOMove(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return transform.DOMove(_original, duration);
        }
    }
}