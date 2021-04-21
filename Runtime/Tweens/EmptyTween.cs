namespace EasyTween
{
    public sealed class EmptyTween : BaseTween
    {
        internal override void Initialize() { }
        internal override void Lerp(float ratio) { }
        internal override float CalculateDurationFromSpeed() => 1.0f;        
    }

}