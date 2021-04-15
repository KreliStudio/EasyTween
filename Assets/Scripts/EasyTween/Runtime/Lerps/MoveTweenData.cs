using UnityEngine;

namespace EasyTween
{
    public sealed class MoveTweenData : TweenData
    {
        Transform target;
        Vector3 startValue;
        Vector3 endValue;

        public MoveTweenData(Transform target, Vector3 value, Space space = Space.Self, float time = 1.0f)
        {
            this.target = target;
            this.time = time;

            startValue = target.position;
            endValue = space == Space.World ? value : startValue + value;
        }

        protected override void Lerp(float ratio)
        {
            var easeFactor = GetEaseFactor(ratio);
            target.position = Vector3.LerpUnclamped(startValue, endValue, easeFactor);
        }
    }
}