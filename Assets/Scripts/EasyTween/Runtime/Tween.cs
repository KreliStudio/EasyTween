using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    public static class Tween
    {
        static TweenHandle tweenHandle;
        public static TweenHandle TweenHandle
        {
            get
            {
                if (tweenHandle == null)
                    tweenHandle = GetOrCreateTweenHandle();
                return tweenHandle;
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

        public static TweenData Move(Transform target, Vector3 value, Space space = Space.Self, float time = 1.0f)
        {
            return new MoveTweenData(target, value, space, time);
        }



        




        // Extensions
        public static void Execute(this TweenData tweenData, float delay = 0)
        {
            TweenHandle.AddTween(tweenData, delay);
        }
        
    }
}