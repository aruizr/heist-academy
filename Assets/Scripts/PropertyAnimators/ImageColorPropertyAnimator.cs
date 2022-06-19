using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PropertyAnimators
{
    public class ImageColorPropertyAnimator : PropertyAnimator<Image, Color>
    {
        private Color _original;

        private void Awake()
        {
            _original = target.color;
        }

        protected override Tweener GetForwardTween()
        {
            return target.DOColor(_original + offset, duration);
        }

        protected override Tweener GetBackwardTween()
        {
            return target.DOColor(_original, duration);
        }
    }
}