using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TestFittsTarget : FittsTarget
{
    private Transform center;
    private Transform[] distractors;

    void Start()
    {
        center = transform.GetChild(0);
        Debug.Log(center);
        distractors = new Transform[transform.childCount-1];
        for(int i = 1; i < transform.childCount; i++)
        {
            distractors[i-1] = transform.GetChild(i);
            Debug.Log(distractors[i-1]);
        }
    }

    public new void SetSize(float size)
    {
        Vector3 width = size * new Vector3(1,1,1);
        center.localScale = width;
        for(int i = 0; i < distractors.Length; i++)
        {
            var target = distractors[i];
            Vector3 dir = (target.position - center.position).normalized;
            target.position = center.position + dir * (target.localScale.x + size * 2);
        }
    }
}

