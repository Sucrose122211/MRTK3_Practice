using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using UnityEngine.UI;

public class PredictionSelection : ISelectionStrategy
{

    public PredictionSelection()
    {
        
    }

    public void OnSelect(out GameObject target)
    {
        target = null;

        GameInstance GI = GameInstance.I;
        if(GI == null || GI.GazeManager == null) return;

        var objects = GameObject.FindObjectsOfType<SelectableObject>();

        float[] probs = new float[objects.Length];
        for(int i = 0; i < objects.Length; i++)
        {
            probs[i] = GetProbability(objects[i].gameObject);
        }
        float s = probs.Sum();
        for(int i = 0; i < objects.Length; i++)
        {
            probs[i] = probs[i] / s * 100;
        }

        Debug.Log(string.Join(", ", probs));
        var tmp = probs.ToList();
        target = objects[tmp.IndexOf(probs.Max())].gameObject;
    }

    private float GetProbability(GameObject obj)
    {
        if(!obj.TryGetComponent<SelectableObject>(out var selectable)) return 0;

        var statistic = new Gaussian(selectable.Width, GameInstance.I.FindManager<FittsManager>().TargetAngle);

        Vector2 pos = Utils.Utils.GetRelativePosition(obj, GameInstance.I.GazeManager.GazeVector, GameInstance.I.GazeManager.GazeOrigin);

        return statistic.GetProbability(pos.x, pos.y);
    }
}
