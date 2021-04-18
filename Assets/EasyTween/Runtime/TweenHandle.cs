﻿using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    internal sealed class TweenHandle : MonoBehaviour
    {
        static TweenHandle instance;
        static TweenHandle Instance
        {
            get
            {
                if (instance == null)
                    instance = GetOrCreateTweenHandle();
                return instance;
            }
        }
        static TweenHandle GetOrCreateTweenHandle()
        {
            TweenHandle handle = UnityEngine.Object.FindObjectOfType<TweenHandle>();
            if (handle != null)
                return handle;

            GameObject gameObject = new GameObject("<Tween Handle>");
            gameObject.hideFlags = HideFlags.NotEditable | HideFlags.HideAndDontSave;
            handle = gameObject.AddComponent<TweenHandle>();
            return handle;
        }


        List<ITweenable> tweens = new List<ITweenable>(capacity: 8);
        

        void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = tweens.Count - 1; i >= 0; i--)
            {
                foreach (var currentTween in tweens[i].CurrentTweens)
                {
                    if (currentTween == null || currentTween.IsCompleted)
                        RemoveTween(currentTween);
                    else
                        currentTween.Update(deltaTime);
                }
            }
        }

        internal static void AddTween(ITweenable tweenData)
        {
            Instance.tweens.Add(tweenData);
        }

        internal static void RemoveTween(ITweenable tweenData)
        {
            Instance.tweens.Remove(tweenData);
        }
    }
}