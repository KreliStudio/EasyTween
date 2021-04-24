using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class TextAlphaTween : BaseTween
    {
        readonly Text target;
        readonly float value;

        float startValue;
        float endValue;

        internal override bool IsValid => target != null;

        public TextAlphaTween(Text target, float value) : base()
        {
            this.target = target;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target.color.a;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            Color newColor = target.color;
            newColor.a = Mathf.LerpUnclamped(startValue, endValue, ratio);
            target.color = newColor;
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Mathf.Abs(endValue - startValue) / speed;
        }
    }
}