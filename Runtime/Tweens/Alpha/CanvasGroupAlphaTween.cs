using UnityEngine;

namespace EasyTween
{
    public sealed class CanvasGroupAlphaTween : BaseTween
    {
        readonly CanvasGroup target;
        readonly float value;

        float startValue;
        float endValue;

        public CanvasGroupAlphaTween(CanvasGroup target, float value) : base()
        {
            this.target = target;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target.alpha;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.alpha = Mathf.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed()
        {
            return Mathf.Abs(endValue - startValue) / Speed;
        }
    }
}