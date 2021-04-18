using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTween;

public class TestingScript : MonoBehaviour
{
    public Color color;

    private void Start()
    {
        var a = EasyTween.Tween
            .Position(transform, Vector3.up, Space.Self)
            .Duration(1.0f);

        var b = EasyTween.Tween
            .Rotation(transform, Vector3.left * 90, Space.Self)
            .Duration(1.0f);

        var c = EasyTween.Tween
            .Color(GetComponent<MeshRenderer>(), Color.red)
            .Duration(2.0f)
            .Ease(EaseType.Linear)
            .Loop(LoopType.PingPong, 2)
            .OnInitialize(() =>
            {
                startPos = transform.position;
            })
            .OnUpdate(OnUpdateTween);

        Tween.Sequence()
            .Append(a,b)
            .Append(c)
            .Execute(1);
    }

    Vector3 startPos;
    
    void OnUpdateTween(float time)
    {
        transform.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.one * 2, time);
        transform.localPosition = Vector3.LerpUnclamped(startPos, startPos + Vector3.up * 2, time);
    }
}
