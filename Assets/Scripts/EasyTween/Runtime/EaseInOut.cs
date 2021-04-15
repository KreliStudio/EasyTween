using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    public static class EaseInOut
    {
        const int precision = 64;

        const float c1 = 1.70158f;
        const float c2 = 2.59491f;
        const float c3 = 2.70158f;
        const float c4 = 2.09439f;
        const float c5 = 1.39626f;
        const float c6 = 7.5625f;
        const float c7 = 2.75f;

        static Dictionary<EaseType, AnimationCurve> cachedCurves;


        public static AnimationCurve GetCurve(EaseType easeType)
        {
            AnimationCurve easeCurve = null;

            if (cachedCurves == null)
                cachedCurves = new Dictionary<EaseType, AnimationCurve>(capacity: 32);
            else if (cachedCurves.TryGetValue(easeType, out easeCurve))
                return easeCurve;

            easeCurve = CreateCurve(easeType);
            cachedCurves.Add(easeType, easeCurve);
            return easeCurve;
        }
                
        public static float Evaluate(EaseType easeType, float x)
        {
            switch (easeType)
            {
                case EaseType.InSine: return CalculateInSine(x);
                case EaseType.OutSine: return CalculateOutSine(x);
                case EaseType.InOutSine: return CalculateInOutSine(x);
                case EaseType.InQuad: return CalculateInQuad(x);
                case EaseType.OutQuad: return CalculateOutQuad(x);
                case EaseType.InOutQuad: return CalculateInOutQuad(x);
                case EaseType.InCubic: return CalculateInCubic(x);
                case EaseType.OutCubic: return CalculateOutCubic(x);
                case EaseType.InOutCubic: return CalculateInOutCubic(x);
                case EaseType.InQuart: return CalculateInQuart(x);
                case EaseType.OutQuart: return CalculateOutQuart(x);
                case EaseType.InOutQuart: return CalculateInOutQuart(x);
                case EaseType.InQuint: return CalculateInQuint(x);
                case EaseType.OutQuint: return CalculateOutQuint(x);
                case EaseType.InOutQuint: return CalculateInOutQuint(x);
                case EaseType.InExpo: return CalculateInExpo(x);
                case EaseType.OutExpo: return CalculateOutExpo(x);
                case EaseType.InOutExpo: return CalculateInOutExpo(x);
                case EaseType.InCirc: return CalculateInCirc(x);
                case EaseType.OutCirc: return CalculateOutCirc(x);
                case EaseType.InOutCirc: return CalculateInOutCirc(x);
                case EaseType.InBack: return CalculateInBack(x);
                case EaseType.OutBack: return CalculateOutBack(x);
                case EaseType.InOutBack: return CalculateInOutBack(x);
                case EaseType.InElastic: return CalculateInElastic(x);
                case EaseType.OutElastic: return CalculateOutElastic(x);
                case EaseType.InOutElastic: return CalculateInOutElastic(x);
                case EaseType.InBounce: return CalculateInBounce(x);
                case EaseType.OutBounce: return CalculateOutBounce(x);
                case EaseType.InOutBounce: return CalculateInOutBounce(x);
                case EaseType.Linear: return x;
                default: return x;
            }
        }


        static AnimationCurve CreateCurve(EaseType easeType)
        {
            Keyframe[] keyframes = new Keyframe[precision];
            float time;

            for (int i = 0; i < precision; i++)
            {
                time = (float)i / precision;
                keyframes[i] = new Keyframe(
                    time: time,
                    value: Evaluate(easeType, time));
            }

            AnimationCurve newCurve = new AnimationCurve(keyframes);
            for (int i = 0; i < precision; i++)
                newCurve.SmoothTangents(i, 0);

            return newCurve;
        }


        static float CalculateInSine(float x)
        {
            return 1.0f - Mathf.Cos((x * Mathf.PI) / 2.0f);
        }
        static float CalculateOutSine(float x)
        {
            return Mathf.Sin((x * Mathf.PI) / 2.0f);
        }
        static float CalculateInOutSine(float x)
        {
            return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
        }
        
        static float CalculateInQuad(float x)
        {
            return x * x;
        }
        static float CalculateOutQuad(float x)
        {
            return 1.0f - (1.0f - x) * (1.0f - x);
        }
        static float CalculateInOutQuad(float x)
        {
            return x < 0.5f ? 2.0f * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f) / 2.0f;
        }

        static float CalculateInCubic(float x)
        {
            return x * x * x;
        }
        static float CalculateOutCubic(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 3.0f);
        }
        static float CalculateInOutCubic(float x)
        {
            return x < 0.5f ? 4.0f * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 3.0f) / 2.0f;
        }

        static float CalculateInQuart(float x)
        {
            return x * x * x * x;
        }
        static float CalculateOutQuart(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 4.0f);
        }
        static float CalculateInOutQuart(float x)
        {
            return x < 0.5f ? 8.0f * x * x * x * x: 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 4.0f) / 2.0f;
        }

        static float CalculateInQuint(float x)
        {
            return x * x * x * x * x;
        }
        static float CalculateOutQuint(float x)
        {
            return 1.0f - Mathf.Pow(1.0f - x, 5.0f);
        }
        static float CalculateInOutQuint(float x)
        {
            return x < 0.5f ? 16.0f * x * x * x * x * x : 1.0f - Mathf.Pow(-2.0f * x + 2.0f, 5.0f) / 2.0f;
        }

        static float CalculateInExpo(float x)
        {
            return x == 0.0f ? 0.0f : Mathf.Pow(2.0f, 10.0f * x - 10.0f);
        }
        static float CalculateOutExpo(float x)
        {
            return x == 1.0f ? 1.0f : 1.0f - Mathf.Pow(2.0f, -10.0f * x);
        }
        static float CalculateInOutExpo(float x)
        {
            return x == 0.0f ? 0.0f : 
                   x == 1.0f ? 1.0f : 
                   x < 0.5f ? Mathf.Pow(2.0f, 20.0f * x - 10.0f) / 2.0f : 
                   (2.0f - Mathf.Pow(2.0f, -20.0f * x + 10.0f)) / 2.0f;
        }

        static float CalculateInCirc(float x)
        {
            return 1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(x, 2.0f));
        }
        static float CalculateOutCirc(float x)
        {
            return Mathf.Sqrt(1.0f - Mathf.Pow(x - 1.0f, 2.0f));
        }
        static float CalculateInOutCirc(float x)
        {
            return x < 0.5f ? (1.0f - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * x, 2.0f))) / 2.0f : 
                   (Mathf.Sqrt(1.0f - Mathf.Pow(-2.0f * x + 2.0f, 2.0f)) + 1.0f) / 2.0f;
        }

        static float CalculateInBack(float x)
        {
            return c3 * x * x * x - c1 * x * x;
        }
        static float CalculateOutBack(float x)
        {
            return 1.0f + c3 * Mathf.Pow(x - 1.0f, 3.0f) + c1 * Mathf.Pow(x - 1.0f, 2.0f);
        }
        static float CalculateInOutBack(float x)
        {
            return x < 0.5f ? (Mathf.Pow(2.0f * x, 2.0f) * ((c2 + 1.0f) * 2.0f * x - c2)) / 2.0f : 
                   (Mathf.Pow(2.0f * x - 2.0f, 2.0f) * ((c2 + 1.0f) * (x * 2.0f - 2.0f) + c2) + 2.0f) / 2.0f;
        }

        static float CalculateInElastic(float x)
        {
            return x == 0.0f ? 0.0f :
                   x == 1.0f ? 1.0f : 
                   -Mathf.Pow(2.0f, 10.0f * x - 10.0f) * Mathf.Sin((x * 10.0f - 10.75f) * c4);
        }
        static float CalculateOutElastic(float x)
        {
            return x == 0.0f ? 0.0f :
                   x == 1.0f ? 1.0f :
                   Mathf.Pow(2.0f, -10.0f * x) * Mathf.Sin((x * 10.0f - 0.75f) * c4) + 1.0f;
        }
        static float CalculateInOutElastic(float x)
        {
            return x == 0.0f ? 0.0f :
                   x == 1.0f ? 1.0f :
                   x < 0.5f ? -(Mathf.Pow(2.0f, 20.0f * x - 10.0f) * Mathf.Sin((20.0f * x - 11.125f) * c5)) / 2.0f :
                   (Mathf.Pow(2.0f, -20.0f * x + 10.0f) * Mathf.Sin((20.0f * x - 11.125f) * c5)) / 2.0f + 1.0f;
        }

        static float CalculateInBounce(float x)
        {
            return 1.0f - CalculateOutBounce(1.0f - x);
        }
        static float CalculateOutBounce(float x)
        {
            if (x < 1 / c7)
            {
                return c6 * x * x;
            }
            else if (x < 2 / c7)
            {
                x -= 1.5f / c7;
                return c6 * x * x + 0.75f;
            }
            else if (x < 2.5 / c7)
            {
                x -= 2.25f / c7;
                return c6 * x * x + 0.9375f;
            }
            else
            {
                x -= 2.625f / c7;
                return c6 * x * x + 0.984375f;
            }
        }
        static float CalculateInOutBounce(float x)
        {
            return x < 0.5f ? (1.0f - CalculateOutBounce(1.0f - 2.0f * x)) / 2.0f : 
                   (1.0f + CalculateOutBounce(2.0f * x - 1)) / 2.0f;
        }
    }
}