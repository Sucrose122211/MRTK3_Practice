using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TestFittsTarget : FittsTarget
{
    [SerializeField] private float distractorSize = 1;
    private Vector3 distractorScale;
    private Transform center;
    private Transform[] distractors;

    void Awake()
    {
        center = transform.GetChild(0);
        distractors = new Transform[transform.childCount-1];
        for(int i = 1; i < transform.childCount; i++)
        {
            distractors[i-1] = transform.GetChild(i);
        }
        distractorScale = distractorSize * new Vector3(1,1,1);
    }

    public override void SetSize(float size)
    {
        Vector3 width = size * new Vector3(1,1,1);
        center.localScale = width;
        for(int i = 0; i < distractors.Length; i++)
        {
            var target = distractors[i];
            target.localScale = distractorScale;
            Vector3 dir = (target.position - center.position).normalized;
            target.position = center.position + dir * (distractorSize * 0.5f + size * 1.5f);
        }
    }

    public override void SetRotation(Quaternion rotation)
    {
        center.rotation = rotation;

        foreach(Transform distractor in distractors)
            distractor.rotation = rotation;
    }
}

