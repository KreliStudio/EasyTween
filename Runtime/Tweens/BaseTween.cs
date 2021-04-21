using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    public abstract class BaseTween : ITweenable
    {
        protected System.Action onInitialize;
        protected System.Action<float> onUpdate;
        protected System.Action onCompleted;
        protected System.Action onStepCompleted;

        public EaseType EaseType { get; protected set; }
        public AnimationCurve CustomEase { get; protected set; }

        public LoopType LoopType { get; protected set; }
        public int LoopAmount { get; protected set; }

        public float Duration { get; protected set; }
        public float Speed { get; protected set; }
        public float ElapsedTime { get; protected set; }

        public float Ratio { get; private set; }
        public float LoopedRatio { get; private set; }
        public float FinalRatio { get; private set; }
        public int CompletedLoops { get; private set; }

        public bool IsCompleted { get; private set; }
        public bool IsInitialized { get; private set; }

        public DeltaTimeType DeltaTimeType { get; private set; }
        
        /// <summary>Return this tween as enumerable.</summary>
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
            EaseType = EaseType.Linear;
            CustomEase = null;
            LoopType = LoopType.None;
            LoopAmount = 1;
            onUpdate = null;
            onCompleted = null;
            onStepCompleted = null;
            IsCompleted = false;
            IsInitialized = false;

            Duration = 1.0f;
            Speed = 0.0f;

            ElapsedTime = 0.0f;
            LoopedRatio = 0.0f;
            FinalRatio = 0.0f;
            CompletedLoops = 0;
        }
        
        /// <summary>Set how long will this tween play.</summary>
        public BaseTween SetDuration(float time)
        {
            Duration = time;
            return this;
        }

        /// <summary>Set speed and it will calculate tween duration.</summary>
        public BaseTween SetDurationFromSpeed(float speed)
        {
            this.Speed = speed;
            return this;
        }
        
        /// <summary>Set ease from buldin types.</summary>
        public BaseTween SetEase(EaseType ease)
        {
            EaseType = ease;
            CustomEase = null;
            return this;
        }

        /// <summary>Set Your own ease using Animation Curve.</summary>
        public BaseTween SetCustomEase(AnimationCurve curve)
        {
            EaseType = EaseType.None;
            CustomEase = curve;
            return this;
        }

        /// <summary>Set loop type. Loop type set to none will disable loop. Amount less or equal 0 will make loop infinite.</summary>
        public BaseTween SetLoop(LoopType loop, int amount = 0)
        {
            LoopType = loop;
            LoopAmount = loop == LoopType.None ? 1 : amount;
            return this;
        }

        /// <summary>Set which delta time will be used for this tween.</summary>
        public BaseTween SetDeltaTime(DeltaTimeType type)
        {
            DeltaTimeType = type;
            return this;
        }
        
        /// <summary>Set callback which will be invoke on initialized tween. Initialization is in first frame of playing tween.</summary>
        public BaseTween OnInitialize(System.Action callback)
        {
            onInitialize = callback;
            return this;
        }

        /// <summary>Set callback which will be invoke on update tween. It is invoke every frame in playing tween. Float parameter in callback is final calculated ratio (contains loop and ease modifiers).</summary>
        public BaseTween OnUpdate(System.Action<float> callback)
        {
            onUpdate = callback;
            return this;
        }

        /// <summary>Set callback which will be invoke on end tween.</summary>
        public BaseTween OnCompleted(System.Action callback)
        {
            onCompleted = callback;
            return this;
        }

        /// <summary>Set callback which will be invoke on end loop step.</summary>
        public BaseTween OnStepCompleted(System.Action callback)
        {
            onStepCompleted = callback;
            return this;
        }



        public override string ToString()
        {
            return $"Tween ({GetType().Name})\n"
                + (Speed > 0 ? $"Speed: {Speed}\n" : $"Duration: {Duration}\n")
                + $"Delta Time: {DeltaTimeType}\n"
                + $"Ease Type: {(CustomEase != null ? "Custom Ease" : EaseType.ToString())}\n"
                + $"Loop: {LoopType.ToString()} {(LoopType != LoopType.None ? "(Amount: " + (LoopAmount < 1 ? "Infinite" : LoopAmount.ToString()) + ")" : string.Empty)}\n"
                + $"Callbacks: {(onInitialize != null ? "OnInitialize " : string.Empty)} {(onUpdate != null ? "OnUpdate " : string.Empty)} {(onCompleted != null ? "OnCompleted " : string.Empty)} {(onStepCompleted != null ? "OnLoopStepCompleted " : string.Empty)}";  
        }



        internal void Update(float deltaTime)
        {
            if (IsCompleted)
                return;

            // setup start values for tween etc
            if (!IsInitialized)
            {
                Initialize();
                onInitialize?.Invoke();
                IsInitialized = true;

                // calculate duration from speed after initialize base tween parameters
                if (Speed > 0.0f)
                    Duration = CalculateDurationFromSpeed();
            }

            // calculate loop ratio and completed loops
            GetLoopedRatio(out var newCompletedLoops);

            // calculate ease ratio
            FinalRatio = GetEaseRatio(LoopedRatio);

            // Lerp tween
            Lerp(FinalRatio);

            // update callback
            onUpdate?.Invoke(FinalRatio);

            // Completed loop callback
            if (CompletedLoops < newCompletedLoops)
            {
                CompletedLoops = newCompletedLoops;
                onStepCompleted?.Invoke();
            }

            // Complete tween if loop amount is achieved
            if (LoopAmount > 0 && CompletedLoops >= LoopAmount)
            {
                Complete();
                return;
            }
            
            // calculate elapsed time & base ratio
            ElapsedTime += deltaTime;
            Ratio = ElapsedTime / Duration;
        }

        void Complete()
        {
            IsCompleted = true;
            onCompleted?.Invoke();
        }

        void GetLoopedRatio(out int completedLoops)
        {
            completedLoops = Mathf.FloorToInt(Ratio);
            switch (LoopType)
            {
                case LoopType.Repeat:
                    LoopedRatio = Ratio % 1.0f;

                    if (this.CompletedLoops != completedLoops)
                        LoopedRatio = Mathf.CeilToInt(LoopedRatio);
                    break;
                case LoopType.PingPong:
                    LoopedRatio = (completedLoops % 2 == 1) ? 1.0f - Ratio % 1.0f : Ratio % 1.0f;

                    if (this.CompletedLoops != completedLoops)
                        LoopedRatio = Mathf.RoundToInt(LoopedRatio);
                    break;
                case LoopType.None:
                default:
                    LoopedRatio = Mathf.Clamp01(Ratio);
                    break;
            }
        }

        float GetEaseRatio(float ratio)
        {
            if (CustomEase != null)
                return CustomEase.Evaluate(ratio);
            else
                return EaseInOut.Evaluate(EaseType, ratio);
        }

        
        internal abstract void Initialize();
        internal abstract float CalculateDurationFromSpeed();
        internal abstract void Lerp(float ratio);        
    }
}