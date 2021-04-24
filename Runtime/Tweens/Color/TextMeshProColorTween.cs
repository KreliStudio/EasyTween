using UnityEngine;
using TMPro;

namespace EasyTween
{
    public sealed class TextMeshProColorTween : BaseTween
    {
        readonly TMP_Text target;
        readonly Color value;

        Color startValue;
        Color endValue;

        internal override bool IsValid => target != null;

        public TextMeshProColorTween(TMP_Text target, Color value) : base()
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