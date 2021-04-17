using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class TextAlphaTween : BaseTween
    {
        Text target;
        float startValue;
        float endValue;

        public TextAlphaTween(Text target, float value) : base()
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