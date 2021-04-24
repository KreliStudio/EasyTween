using UnityEngine;

namespace EasyTween
{
    public sealed class AnchoredPosition3DTween : BaseTween
    {
        readonly RectTransform target;
        readonly Vector3 value;

        Vector3 startValue;
        Vector3 endValue;

        internal override bool IsValid => target != null;

        public AnchoredPosition3DTween(RectTransform target, Vector3 value) : base()
        {
            this.target = target;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target.anchoredPosition3D;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            target.anchoredPosition3D = Vector3.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Vector3.Distance(endValue, startValue) / speed;
        }
    }
}