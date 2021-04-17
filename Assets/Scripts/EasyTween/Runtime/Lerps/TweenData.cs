using UnityEngine;

namespace EasyTween
{
    [System.Serializable]
    public abstract class TweenData
    {
        protected EaseType easeType;
        protected AnimationCurve customEase;

        protected LoopType loopType;
        protected int loopAmount;

        protected System.Action onCompleted;
        protected System.Action<float> onUpdate;
        protected System.Action onLoopStepCompleted;


        protected float duration;
        protected float elapsedTime;

        float ratio;
        float loopedRatio;
        float easeRatio;
        int completedLoops;


        protected TweenData()
        {
            // set default values
            easeType = EaseType.Linear;
            customEase = null;
            loopType = LoopType.None;
            loopAmount = 1;
            onUpdate = null;
            onCompleted = null;
            onLoopStepCompleted = null;

            duration = 1.0f;

            elapsedTime = 0.0f;
            loopedRatio = 0.0f;
            easeRatio = 0.0f;
            completedLoops = 0;
        }
        

        public TweenData Duration(float time)
        {
            duration = time;
            return this;
        }

        public TweenData Ease(EaseType ease)
        {
            easeType = ease;
            customEase = null;
            return this;
        }

        public TweenData CustomEase(AnimationCurve curve)
        {
            easeType = EaseType.None;
            customEase = curve;
            return this;
        }

        public TweenData Loop(LoopType loop, int amount = 0)
        {
            loopType = loop;
            loopAmount = loop == LoopType.None ? 1 : amount;
            return this;
        }
        
        public TweenData OnUpdate(System.Action<float> callback)
        {
            onUpdate = callback;
            return this;
        }

        public TweenData OnCompleted(System.Action callback)
        {
            onCompleted = callback;
            return this;
        }

        public TweenData OnLoopStepCompleted(System.Action callback)
        {
            onLoopStepCompleted = callback;
            return this;
        }



        internal void Update(float deltaTime)
        {
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
                if (onLoopStepCompleted != null)
                    onLoopStepCompleted();
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
            if (onCompleted != null)
                onCompleted();

            TweenHandle.RemoveTween(this);
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



        internal abstract void Lerp(float ratio);

        public override string ToString()
        {
            return "Tween (" + GetType().Name + ")\n"
                + "Duration: " + duration + "s\n"
                + "EaseType: " + (customEase != null ? "Custom Ease" : easeType.ToString()) + "\n"
                + "Loop: " + loopType.ToString() + " (amount: " + (loopAmount == -1 ? "Infinite" : loopAmount.ToString()) + ")\n"
                + "Callbacks: "
                    + (onUpdate != null ? "OnUpdate " : string.Empty)
                    + (onCompleted != null ? "OnCompleted " : string.Empty)
                    + (onLoopStepCompleted != null ? "OnLoopStepCompleted " : string.Empty);
        }

    }
}