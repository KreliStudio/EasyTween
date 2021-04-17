using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class TextColorTween : BaseTween
    {
        Text target;
        Color startValue;
        Color endValue;

        public TextColorTween(Text target, Color value) : base()
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