using UnityEngine;

namespace EasyTween
{
    public sealed class AnchorMinTween : BaseTween
    {
        readonly RectTransform target;
        readonly Vector2 value;

        Vector2 startValue;
        Vector2 endValue;

        internal override bool IsValid => target != null;

        public AnchorMinTween(RectTransform target, Vector2 value) : base()
        {
            this.target = target;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target.anchorMin;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.anchorMin = Vector2.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Vector2.Distance(endValue, startValue) / speed;
        }
    }
}