using UnityEngine;

namespace EasyTween
{
    public sealed class MeshRendererAlphaTween : BaseTween
    {
        readonly Material target;
        readonly float value;

        float startValue;
        float endValue;

        internal override bool IsValid => target != null;

        public MeshRendererAlphaTween(MeshRenderer target, float value) : base()
        {
            this.target = target.material;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target != null ? target.color.a : 1.0f;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            if (target == null)
                return;

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