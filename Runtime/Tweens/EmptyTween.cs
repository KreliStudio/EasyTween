namespace EasyTween
{
    public sealed class EmptyTween : BaseTween
    {
        internal override bool IsValid => true;
        internal override void Initialize() { }
        internal override void Lerp(float ratio) { }
        internal override float CalculateDurationFromSpeed(float speed) => 1.0f;
    }

}