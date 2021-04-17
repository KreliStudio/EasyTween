using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyTween
{
    public static class Tween
    {

        public static TweenData Position(Transform target, Vector3 value, Space space = Space.Self)
        {
            return new PositionTweenData(target, value, space);
        }



        




        // Extensions
        public static void Execute(this TweenData tweenData, float delay = 0)
        {
            TweenHandle.AddTween(tweenData);
        }
        
    }
}