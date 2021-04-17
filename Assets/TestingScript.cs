using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTween;

public class TestingScript : MonoBehaviour
{
    public AnimationCurve curve;
    public TweenData tweenData;

    private void Start()
    {
        EasyTween.Tween
            .Position(transform, Vector3.up, Space.World)
            .Duration(1.0f)
            .Loop(LoopType.Repeat)
            .Execute();
    }
    

}
