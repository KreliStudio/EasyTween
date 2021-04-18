﻿using UnityEngine;

namespace EasyTween
{
    public sealed class MeshRendererColorTween : BaseTween
    {
        readonly Material target;
        readonly Color value;

        Color startValue;
        Color endValue;

        public MeshRendererColorTween(MeshRenderer target, Color value) : base()
        {
            this.target = target.material;
            this.value = value;
        }

        internal override void Initialize()
        {
            startValue = target != null ? target.color : Color.white;
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