using UnityEngine;

namespace EasyTween
{
    public sealed class EmptyTween : BaseTween
    {
        internal override void Lerp(float ratio) { }
    }
}