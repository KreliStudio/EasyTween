using UnityEngine;

namespace EasyTween
{
    public abstract class TweenData
    {
        protected EaseType easeType;
        protected AnimationCurve customEase;

        protected LoopType loopType;
        protected int loopAmount;

        protected System.Action onCompleted;

        protected float time;

        public TweenData Ease(EaseType ease)
        {
            easeType = ease;
            customEase = null;
            return this;
        }

        public TweenData CustomEase(AnimationCurve curve)
        {
            customEase = curve;
            return this;
        }

        public TweenData Loop(LoopType loop, int amount = -1)
        {
            loopType = loop;
            loopAmount = amount;
            return this;
        }

        public TweenData OnCompleted(System.Action callback)
        {
            onCompleted = callback;
            return this;
        }


        protected float GetEaseFactor(float ratio)
        {
            AnimationCurve ease = customEase ?? EaseInOut.GetCurve(easeType);
            return ease != null ? ease.Evaluate(ratio) : ratio;
        }

        protected abstract void Lerp(float ratio);
    }
}