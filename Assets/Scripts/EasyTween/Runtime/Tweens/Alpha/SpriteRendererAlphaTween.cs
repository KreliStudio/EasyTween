using UnityEngine;

namespace EasyTween
{
    public sealed class SpriteRendererAlphaTween : BaseTween
    {
        SpriteRenderer target;
        float startValue;
        float endValue;

        public SpriteRendererAlphaTween(SpriteRenderer target, float value) : base()
        {
            this.target = target;
            startValue = target.color.a;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            Color newColor = target.color;
            newColor.a = Mathf.LerpUnclamped(startValue, endValue, ratio);
            target.color = newColor;
        }
    }
}