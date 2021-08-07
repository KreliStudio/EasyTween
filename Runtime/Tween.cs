using UnityEngine;
using UnityEngine.UI;

namespace EasyTween
{
    public static class Tween
    {
        /// <summary>Create position tween.</summary>
        /// <param name="target">Target object which will be moved.</param>
        /// <param name="value">Vector where target should be moved.</param>
        /// <param name="space">In what space should be calculated movement.</param>
        /// <returns>Position Tween</returns>
        public static BaseTween Position(in Transform target, in Vector3 value, in Space space = Space.Self)
            => new PositionTween(target, value, space);

        /// <summary>Create rotation tween.</summary>
        /// <param name="target">Target object which will be rotated.</param>
        /// <param name="value">Euler Angles how target should be rotated.</param>
        /// <param name="space">In what space should be calculated rotation.</param>
        /// <returns>Rotation Tween</returns>
        public static BaseTween Rotation(in Transform target, in Vector3 value, in Space space = Space.Self)
            => Rotation(target, Quaternion.Euler(value), space);

        /// <summary>Create rotation tween.</summary>
        /// <param name="target">Target object which will be rotated.</param>
        /// <param name="value">Quaternion how target should be rotated.</param>
        /// <param name="space">In what space should be calculated rotation.</param>
        /// <returns>Rotation Tween</returns>
        public static BaseTween Rotation(in Transform target, in Quaternion value, in Space space = Space.Self)
            => new RotationTween(target, value, space);

        /// <summary>Create scale tween.</summary>
        /// <param name="target">Target object which will be scaled.</param>
        /// <param name="value">Vector how target should be scaled.</param>
        /// <param name="space">In what space should be calculated scale.</param>
        /// <returns>Scale Tween</returns>
        public static BaseTween Scale(in Transform target, in Vector3 value, in Space space = Space.Self)
            => new ScaleTween(target, value, space);
        
        /// <summary>Create Empty Tween. It is useful for custom tweens.</summary>
        /// <returns>Empty Tween</returns>
        public static BaseTween Empty()
            => new EmptyTween();

        /// <summary>Create Empty tween with duration.</summary>
        /// <param name="duration">How much should play.</param>
        /// <returns>Empty Tween</returns>
        public static BaseTween Delay(in float duration)
            => Empty().SetDuration(duration);        
       
        /// <summary>Create color tween for Image component.</summary>
        /// <param name="target">Target Image compoennt.</param>
        /// <param name="value">Color it will turn into.</param>
        /// <returns>Image Color Tween</returns>
        public static BaseTween Color(in Image target, in Color value)
            => new ImageColorTween(target, value);
        
        /// <summary>Create color tween for Text component.</summary>
        /// <param name="target">Target Text compoennt.</param>
        /// <param name="value">Color it will turn into.</param>
        /// <returns>Text Color Tween</returns>
        public static BaseTween Color(in Text target, in Color value)
            => new TextColorTween(target, value);

        /// <summary>Create color tween for TextMeshPro component.</summary>
        /// <param name="target">Target TextMeshPro compoennt.</param>
        /// <param name="value">Color it will turn into.</param>
        /// <returns>TextMeshPro Color Tween</returns>
        public static BaseTween Color(in TMPro.TMP_Text target, in Color value)
            => new TextMeshProColorTween(target, value);

        /// <summary>Create color tween for MeshRenderer component.</summary>
        /// <param name="target">Target MeshRenderer compoennt.</param>
        /// <param name="value">Color it will turn into.</param>
        /// <returns>MeshRenderer Color Tween</returns>
        public static BaseTween Color(in MeshRenderer target, in Color value)
            => new MeshRendererColorTween(target, value);

        /// <summary>Create color tween for SpriteRenderer component.</summary>
        /// <param name="target">Target SpriteRenderer compoennt.</param>
        /// <param name="value">Color it will turn into.</param>
        /// <returns>SpriteRenderer Color Tween</returns>
        public static BaseTween Color(in SpriteRenderer target, in Color value)
            => new SpriteRendererColorTween(target, value);

        /// <summary>Create alpha tween for SpriteRenderer component.</summary>
        /// <param name="target">Target SpriteRenderer compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>SpriteRenderer Alpha Tween</returns>
        public static BaseTween Alpha(in SpriteRenderer target, in float value)
            => new SpriteRendererAlphaTween(target, value);

        /// <summary>Create alpha tween for MeshRenderer component.</summary>
        /// <param name="target">Target MeshRenderer compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>MeshRenderer Alpha Tween</returns>
        public static BaseTween Alpha(in MeshRenderer target, in float value)
            => new MeshRendererAlphaTween(target, value);

        /// <summary>Create alpha tween for Text component.</summary>
        /// <param name="target">Target Text compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>Text Alpha Tween</returns>
        public static BaseTween Alpha(in Text target, in float value)
            => new TextAlphaTween(target, value);

        /// <summary>Create alpha tween for TextMeshPro component.</summary>
        /// <param name="target">Target TextMeshPro compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>TextMeshPro Alpha Tween</returns>
        public static BaseTween Alpha(in TMPro.TMP_Text target, in float value)
            => new TextMeshProAlphaTween(target, value);

        /// <summary>Create alpha tween for Image component.</summary>
        /// <param name="target">Target Image compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>Image Alpha Tween</returns>
        public static BaseTween Alpha(in Image target, in float value)
            => new ImageAlphaTween(target, value);

        /// <summary>Create alpha tween for CanvasGroup component.</summary>
        /// <param name="target">Target CanvasGroup compoennt.</param>
        /// <param name="value">Alpha it will turn into.</param>
        /// <returns>CanvasGroup Alpha Tween</returns>
        public static BaseTween Alpha(in CanvasGroup target, in float value)
            => new CanvasGroupAlphaTween(target, value);

        /// <summary>Create tween sequence.</summary>
        /// <returns>Sequence</returns>
        public static Sequence Sequence()
            => new Sequence();
        

        // Extensions

        /// <summary>Execute method will add this tween to TweenHandle and start playing it.</summary>
        /// <param name="delay">Delay before tween.</param>
        public static void Execute(this BaseTween tween, in float delay = 0)
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

        /// <summary>Execute method will add this sequence to TweenHandle and start playing it.</summary>
        /// <param name="delay">Delay before tween.</param>
        public static void Execute(this Sequence sequence, in float delay = 0)
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

    public enum DeltaTimeType
    {
        DeltaTime = 0,
        UnscaledDeltaTime,
        FixedDeltaTime,
        FixedUnscaledDeltaTime,
        SmoothDeltaTime,
    }
}