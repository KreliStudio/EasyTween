using UnityEngine;

namespace EasyTween
{
    public sealed class PositionTween : BaseTween
    {
        Transform target;
        Vector3 startValue;
        Vector3 endValue;

        public PositionTween(Transform target, Vector3 value, Space space = Space.Self) : base()
        {
            this.target = target;
            startValue = target.position;
            endValue = space == Space.World ? value : startValue + value;
        }

        internal override void Lerp(float ratio)
        {
            target.position = Vector3.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}