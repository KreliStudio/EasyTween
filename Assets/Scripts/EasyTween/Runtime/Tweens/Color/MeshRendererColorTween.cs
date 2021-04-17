using UnityEngine;

namespace EasyTween
{
    public sealed class MeshRendererColorTween : BaseTween
    {
        Material target;
        Color startValue;
        Color endValue;

        public MeshRendererColorTween(MeshRenderer target, Color value) : base()
        {
            this.target = target.material;
            startValue = this.target != null ? this.target.color : Color.white;
            endValue = value;
        }

        internal override void Lerp(float ratio)
        {
            if (target == null)
                return;

            target.color = Color.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}