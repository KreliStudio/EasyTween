using UnityEngine;

namespace EasyTween
{
    public sealed class RotationTween : BaseTween
    {
        readonly Transform target;
        readonly Quaternion value;
        readonly Space space;

        Quaternion startValue;
        Quaternion endValue;
        
        public RotationTween(Transform target, Quaternion value, Space space = Space.Self) : base()
        {
            this.target = target;
            this.value = value;
            this.space = space;
        }

        internal override void Initialize()
        {
            startValue = target.rotation;
            endValue = space == Space.World ? value : startValue * value;
        }

        internal override void Lerp(float ratio)
        {
            target.rotation = Quaternion.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed()
        {
            return Quaternion.Angle(endValue, startValue) / speed;
        }
    }
}