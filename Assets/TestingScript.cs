using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTween;

public class TestingScript : MonoBehaviour
{
    public Color color;

    private void Start()
    {
        /*EasyTween.Tween
            .Position(transform, Vector3.up, Space.World)
            .Duration(1.0f)
            .Loop(LoopType.Repeat)
            .Execute();*/
        EasyTween.Tween
            .Alpha(GetComponent<MeshRenderer>(), 0)
            .Duration(1.0f)
            .Ease(EaseType.Linear)
            .OnUpdate(OnUpdateTween)
            .Execute();

        

    }
    
    void OnUpdateTween(float time)
    {
        //transform.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.one * 2, time);
       // transform.localPosition = Vector3.LerpUnclamped(Vector3.zero, Vector3.up, time);
    }
}
