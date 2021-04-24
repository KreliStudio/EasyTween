using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    internal sealed class TweenHandle : MonoBehaviour
    {        
        static TweenHandle instance;
        /// <summary>Singleton & scene object representation.</summary>
        static TweenHandle Instance
        {
            get
            {
                if (instance == null)
                {
                    // try find existing instance
                    instance = FindObjectOfType<TweenHandle>();
                    if (instance != null)
                        return instance;
                    // or create new one
                    GameObject gameObject = new GameObject("<Tween Handle>");
                    gameObject.hideFlags = HideFlags.NotEditable | HideFlags.HideAndDontSave;
                    instance = gameObject.AddComponent<TweenHandle>();
                }
                return instance;
            }
        }

        /// <summary>List of all current playing tweenable.</summary>
        List<ITweenable> tweens = new List<ITweenable>(capacity: 8);
        

        void Update()
        {
            for (int i = tweens.Count - 1; i >= 0; i--)
                foreach (var currentTween in tweens[i].CurrentTweens)
                    if (currentTween == null || !currentTween.IsValid || currentTween.IsCompleted)
                        RemoveTween(currentTween);
                    else
                        currentTween.Update(GetDeltaTime(currentTween.DeltaTimeType));
        }
        

        /// <summary>Internal method adds new tweenable to handle.</summary>
        internal static void AddTween(ITweenable tweenData)
            => Instance.tweens.Add(tweenData);
        

        /// <summary>Internal method removes tweenable from handle.</summary>
        internal static void RemoveTween(ITweenable tweenData)
            => Instance.tweens.Remove(tweenData);
        
        /// <summary>Method returns the time depending on the given type.</summary>
        float GetDeltaTime(DeltaTimeType type)
        {
            switch (type)
            {
                case DeltaTimeType.UnscaledDeltaTime: return Time.unscaledDeltaTime;
                case DeltaTimeType.FixedDeltaTime: return Time.fixedDeltaTime;
                case DeltaTimeType.FixedUnscaledDeltaTime: return Time.fixedUnscaledDeltaTime;
                case DeltaTimeType.SmoothDeltaTime: return Time.smoothDeltaTime;
                case DeltaTimeType.DeltaTime:
                default: return Time.deltaTime;
            }
        }


    }
}