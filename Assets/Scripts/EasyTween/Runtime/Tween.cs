using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public static class Tween
    {
        public static BaseTween Position(Transform target, Vector3 value, Space space = Space.Self)
        {
            return new PositionTween(target, value, space);
        }

        public static BaseTween Rotation(Transform target, Vector3 value, Space space = Space.Self)
        {
            return Rotation(target, Quaternion.Euler(value), space);
        }
        public static BaseTween Rotation(Transform target, Quaternion value, Space space = Space.Self)
        {
            return new RotationTween(target, value, space);
        }

        public static BaseTween Scale(Transform target, Vector3 value, Space space = Space.Self)
        {
            return new ScaleTween(target, value, space);
        }

        public static BaseTween Empty()
        {
            return new EmptyTween();
        }
        public static BaseTween Delay(float duration)
        {
            return Empty().Duration(duration);
        }
        
        public static BaseTween Color(Image target, Color value)
        {
            return new ImageColorTween(target, value);
        }
        public static BaseTween Color(Text target, Color value)
        {
            return new TextColorTween(target, value);
        }
        public static BaseTween Color(MeshRenderer target, Color value)
        {
            return new MeshRendererColorTween(target, value);
        }
        public static BaseTween Color(SpriteRenderer target, Color value)
        {
            return new SpriteRendererColorTween(target, value);
        }
        
        public static BaseTween Alpha(SpriteRenderer target, float value)
        {
            return new SpriteRendererAlphaTween(target, value);
        }
        public static BaseTween Alpha(MeshRenderer target, float value)
        {
            return new MeshRendererAlphaTween(target, value);
        }
        public static BaseTween Alpha(Text target, float value)
        {
            return new TextAlphaTween(target, value);
        }
        public static BaseTween Alpha(Image target, float value)
        {
            return new ImageAlphaTween(target, value);
        }
        public static BaseTween Alpha(CanvasGroup target, float value)
        {
            return new CanvasGroupAlphaTween(target, value);
        }
        

        public static Sequence Sequence()
        {
            return new Sequence();
        }

        // Extensions
        public static void Execute(this BaseTween tween, float delay = 0)
        {
            if (delay > 0)
            {
                TweenHandle.AddTween(
                   Sequence()
                    .Append(Delay(delay))
                    .Append(tween)
                    );                
            }
            else
            {
                TweenHandle.AddTween(tween);
            }
        }

        public static void Execute(this Sequence sequence, float delay = 0)
        {
            if (delay > 0)
                sequence.Prepend(Delay(delay));            

            TweenHandle.AddTween(sequence);
        }

    }

    public enum EaseType
    {
        None = -1,
        Linear = 0,

        InSine,
        OutSine,
        InOutSine,

        InQuad,
        OutQuad,
        InOutQuad,

        InCubic,
        OutCubic,
        InOutCubic,

        InQuart,
        OutQuart,
        InOutQuart,

        InQuint,
        OutQuint,
        InOutQuint,

        InExpo,
        OutExpo,
        InOutExpo,

        InCirc,
        OutCirc,
        InOutCirc,

        InBack,
        OutBack,
        InOutBack,

        InElastic,
        OutElastic,
        InOutElastic,

        InBounce,
        OutBounce,
        InOutBounce,
    }

    public enum LoopType
    {
        None = 0,
        Repeat,
        PingPong,
    }
}