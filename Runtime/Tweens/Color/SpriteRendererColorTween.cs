using UnityEngine;

namespace EasyTween
{
    public sealed class SpriteRendererColorTween : BaseTween
    {
        readonly SpriteRenderer target;
        readonly Color value;
        
        Color startValue;
        Color endValue;

        internal override bool IsValid => target != null;

        public SpriteRendererColorTween(SpriteRenderer target, Color value) : base()
        {
            this.target = target;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target.color;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.color = Color.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Vector4.Distance(endValue, startValue) / speed;
        }
    }
}