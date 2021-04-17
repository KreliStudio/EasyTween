using UnityEngine;

namespace EasyTween
{
    public sealed class RotationTween : BaseTween
    {
        Transform target;
        Quaternion startValue;
        Quaternion endValue;
        
        public RotationTween(Transform target, Quaternion value, Space space = Space.Self) : base()
        {
            this.target = target;
            startValue = target.rotation;
            endValue = space == Space.World ? value : startValue * value;
        }

        internal override void Lerp(float ratio)
        {
            target.rotation = Quaternion.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}