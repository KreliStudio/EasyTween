using System;
using UnityEngine;

namespace EasyTween
{
    public sealed class TweenHandle : MonoBehaviour
    {
        void Update()
        {

        }

        public void AddTween(TweenData tweenData, float delay)
        {
            Debug.Log(tweenData.ToString());
        }
    }
}