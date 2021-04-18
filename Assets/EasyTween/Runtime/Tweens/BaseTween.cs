using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    public abstract class BaseTween : ITweenable
    {
        protected EaseType easeType;
        protected AnimationCurve customEase;

        protected LoopType loopType;
        protected int loopAmount;

        protected System.Action onInitialize;
        protected System.Action<float> onUpdate;
        protected System.Action onCompleted;
        protected System.Action onStepCompleted;


        protected float duration;
        protected float elapsedTime;

        float ratio;
        float loopedRatio;
        float easeRatio;
        int completedLoops;

        public bool IsCompleted { get; private set; }
        public bool IsInitialized { get; private set; }
        
        public IEnumerable<BaseTween> CurrentTweens
        {
            get
            {
                yield return this;
            }
        }

        protected BaseTween()
        {
            // set default values
            easeType = EaseType.Linear;
            customEase = null;
            loopType = LoopType.None;
            loopAmount = 1;
            onUpdate = null;
            onCompleted = null;
            onStepCompleted = null;
            IsCompleted = false;
            IsInitialized = false;

            duration = 1.0f;

            elapsedTime = 0.0f;
            loopedRatio = 0.0f;
            easeRatio = 0.0f;
            completedLoops = 0;
        }
        

        public BaseTween Duration(float time)
        {
            duration = time;
            return this;
        }

        public BaseTween Ease(EaseType ease)
        {
            easeType = ease;
            customEase = null;
            return this;
        }

        public BaseTween CustomEase(AnimationCurve curve)
        {
            easeType = EaseType.None;
            customEase = curve;
            return this;
        }

        public BaseTween Loop(LoopType loop, int amount = 0)
        {
            loopType = loop;
            loopAmount = loop == LoopType.None ? 1 : amount;
            return this;
        }

        public BaseTween OnInitialize(System.Action callback)
        {
            onInitialize = callback;
            return this;
        }

        public BaseTween OnUpdate(System.Action<float> callback)
        {
            onUpdate = callback;
            return this;
        }
                
        public BaseTween OnCompleted(System.Action callback)
        {
            onCompleted = callback;
            return this;
        }

        public BaseTween OnStepCompleted(System.Action callback)
        {
            onStepCompleted = callback;
            return this;
        }

        public override string ToString()
        {
            return "Tween (" + GetType().Name + ")\n"
                + "Duration: " + duration + "s\n"
                + "EaseType: " + (customEase != null ? "Custom Ease" : easeType.ToString()) + "\n"
                + "Loop: " + loopType.ToString() + (loopType != LoopType.None ? " (Amount: " + (loopAmount < 1 ? "Infinite" : loopAmount.ToString()) + ")" : string.Empty) +"\n"
                + "Callbacks: "
                    + (onUpdate != null ? "OnUpdate " : string.Empty)
                    + (onCompleted != null ? "OnCompleted " : string.Empty)
                    + (onStepCompleted != null ? "OnLoopStepCompleted " : string.Empty);
        }



        internal void Update(float deltaTime)
        {
            if (IsCompleted)
                return;

            // setup start values for tween etc
            if (!IsInitialized)
            {
                Initialize();
                if (onInitialize != null)
                    onInitialize();
                IsInitialized = true;
            }

            // calculate loop ratio and completed loops
            int newCompletedLoops;
            GetLoopedRatio(ratio, out loopedRatio, out newCompletedLoops);
            
            // calculate ease ratio
            easeRatio = GetEaseRatio(loopedRatio);

            // Lerp tween
            Lerp(easeRatio);

            // update callback
            if (onUpdate != null)
                onUpdate(easeRatio);
            
            // Completed loop callback
            if (completedLoops < newCompletedLoops)
            {
                completedLoops = newCompletedLoops;
                if (onStepCompleted != null)
                    onStepCompleted();
            }

            // Complete tween if loop amount is achieved
            if (loopAmount > 0 && completedLoops >= loopAmount)
            {
                Complete();
                return;
            }
            
            // calculate elapsed time & base ratio
            elapsedTime += deltaTime;
            ratio = elapsedTime / duration;
        }

        void Complete()
        {
            IsCompleted = true;

            if (onCompleted != null)
                onCompleted();
        }

        void GetLoopedRatio(float ratio, out float loopedRatio, out int completedLoops)
        {
            completedLoops = Mathf.FloorToInt(ratio);
            switch (loopType)
            {
                case LoopType.Repeat:
                    loopedRatio = ratio % 1.0f;

                    if (this.completedLoops != completedLoops)
                        loopedRatio = Mathf.CeilToInt(loopedRatio);
                    break;
                case LoopType.PingPong:
                    loopedRatio = (completedLoops % 2 == 1) ? 1.0f - ratio % 1.0f : ratio % 1.0f;

                    if (this.completedLoops != completedLoops)
                        loopedRatio = Mathf.RoundToInt(loopedRatio);
                    break;
                case LoopType.None:
                default:
                    loopedRatio = Mathf.Clamp01(ratio);
                    break;
            }
        }

        float GetEaseRatio(float ratio)
        {
            if (customEase != null)
                return customEase.Evaluate(ratio);
            else
                return EaseInOut.Evaluate(easeType, ratio);
        }

        
        internal abstract void Initialize();        
        internal abstract void Lerp(float ratio);        
    }
}