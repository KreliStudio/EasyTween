using UnityEngine;

namespace EasyTween
{
    public sealed class OffsetMaxTween : BaseTween
    {
        readonly RectTransform target;
        readonly Vector2 value;
        readonly Space space;

        Vector2 startValue;
        Vector2 endValue;

        internal override bool IsValid => target != null;

        public OffsetMaxTween(RectTransform target, Vector2 value, Space space = Space.Self) : base()
        {
            this.target = target;
            this.value = value;
            this.space = space;
        }

        internal override void Initialize()
        {
            startValue = target.offsetMax;
            endValue = space == Space.World ? value : startValue + value;
        }

        internal override void Lerp(float ratio)
        {
            target.offsetMax = Vector2.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Vector2.Distance(endValue, startValue) / speed;
        }
    }
}