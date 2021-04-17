using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class ImageColorTween : BaseTween
    {
        Image target;
        Color startValue;
        Color endValue;

        public ImageColorTween(Image target, Color value) : base()
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