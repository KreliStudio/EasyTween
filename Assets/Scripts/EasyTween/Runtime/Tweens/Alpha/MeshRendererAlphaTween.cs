using UnityEngine;

namespace EasyTween
{
    public sealed class MeshRendererAlphaTween : BaseTween
    {
        Material target;
        float startValue;
        float endValue;

        public MeshRendererAlphaTween(MeshRenderer target, float value) : base()
        {
            this.target = target.material;
            startValue = this.target != null ? this.target.color.a : 1.0f;
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
    }
}