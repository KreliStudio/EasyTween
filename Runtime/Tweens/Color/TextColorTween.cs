using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class TextColorTween : BaseTween
    {
        readonly Text target;
        readonly Color value;

        Color startValue;
        Color endValue;

        public TextColorTween(Text target, Color value) : base()
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

        internal override float CalculateDurationFromSpeed()
        {
            return Vector4.Distance(endValue, startValue) / Speed;
        }
    }
}