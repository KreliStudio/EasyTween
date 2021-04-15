using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTween;

public class TestingScript : MonoBehaviour
{
    public AnimationCurve[] animationCurve;
    


    private void Start()
    {
        EasyTween.Tween
            .Move(transform, Vector3.up, Space.Self, 0.25f)
            .Ease(EaseType.Linear)
            .Loop(LoopType.PingPong)
            .OnCompleted(() => { Debug.Log("Tween Completed"); })
            .Execute();

    }

}
