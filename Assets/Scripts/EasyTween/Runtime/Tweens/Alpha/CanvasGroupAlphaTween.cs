using UnityEngine;

namespace EasyTween
{
    public sealed class CanvasGroupAlphaTween : BaseTween
    {
        CanvasGroup target;
        float startValue;
        float endValue;

        public CanvasGroupAlphaTween(CanvasGroup target, float value) : base()
        {
            this.target = target;
            startValue = target.alpha;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.alpha = Mathf.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}