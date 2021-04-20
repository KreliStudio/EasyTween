using UnityEngine;
using TMPro;

namespace EasyTween
{
    public sealed class TextMeshProAlphaTween : BaseTween
    {
        readonly TMP_Text target;
        readonly float value;

        float startValue;
        float endValue;

        public TextMeshProAlphaTween(TMP_Text target, float value) : base()
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

        internal override float CalculateDurationFromSpeed()
        {
            return Mathf.Abs(endValue - startValue) / speed;
        }
    }
}