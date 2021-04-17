using UnityEngine;

namespace EasyTween
{
    public sealed class ScaleTween : BaseTween
    {
        Transform target;
        Vector3 startValue;
        Vector3 endValue;
        

        public ScaleTween(Transform target, Vector3 value, Space space = Space.Self) : base()
        {
            this.target = target;
            startValue = target.localScale;

            if (space == Space.World)
            {
                Vector3 worldScale = target.lossyScale;
                Vector3 localScale = target.localScale;

                endValue = new Vector3(
                    x: value.x * (localScale.x / Mathf.Max(worldScale.x,float.Epsilon)),
                    y: value.y * (localScale.y / Mathf.Max(worldScale.y, float.Epsilon)),
                    z: value.z * (localScale.z / Mathf.Max(worldScale.z, float.Epsilon))
                    );
            }
            else
            {
                endValue = value;
            }

        }

        internal override void Lerp(float ratio)
        {
            target.localScale = Vector3.LerpUnclamped(startValue, endValue, ratio);
        }
    }
}