namespace EasyTween
{
    public interface ITweenable
    {
        /// <summary>Return all tweens which should be playing. It is used in Base Tween class and returns it self and in sequence and returns all current playing parallel tweens from sequence.</summary>
        System.Collections.Generic.IEnumerable<BaseTween> CurrentTweens { get; }
    }
}