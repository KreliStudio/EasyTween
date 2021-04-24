using UnityEngine;

namespace EasyTween
{
    public sealed class PositionTween : BaseTween
    {
        readonly Transform target;
        readonly Vector3 value;
        readonly Space space;

        Vector3 startValue;
        Vector3 endValue;

        internal override bool IsValid => target != null;

        public PositionTween(Transform target, Vector3 value, Space space = Space.Self) : base()
        {
            this.target = target;
            this.value = value;
            this.space = space;
        }

        internal override void Initialize()
        {
            startValue = target.position;
            endValue = space == Space.World ? value : startValue + value;
        }

        internal override void Lerp(float ratio)
        {
            target.position = Vector3.LerpUnclamped(startValue, endValue, ratio);
        }

        internal override float CalculateDurationFromSpeed(float speed)
        {
            return Vector3.Distance(endValue, startValue) / speed;
        }
    }
}