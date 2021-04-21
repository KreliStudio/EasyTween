namespace EasyTween
{
    public interface ITweenable
    {
        System.Collections.Generic.IEnumerable<BaseTween> CurrentTweens { get; }
    }
}