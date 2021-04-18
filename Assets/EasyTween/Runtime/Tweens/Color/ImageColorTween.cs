﻿using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public sealed class ImageColorTween : BaseTween
    {
        readonly Image target;
        readonly Color value;

        Color startValue;
        Color endValue;

        public ImageColorTween(Image target, Color value) : base()
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
    }
}