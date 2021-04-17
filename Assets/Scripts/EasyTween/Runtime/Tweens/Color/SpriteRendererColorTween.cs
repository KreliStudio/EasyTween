using UnityEngine;

namespace EasyTween
{
    public sealed class SpriteRendererColorTween : BaseTween
    {
        SpriteRenderer target;
        Color startValue;
        Color endValue;

        public SpriteRendererColorTween(SpriteRenderer target, Color value) : base()
        {
            this.target = target;
            startValue = target.color;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.color = Color.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}