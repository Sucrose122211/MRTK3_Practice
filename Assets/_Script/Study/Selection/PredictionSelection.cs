using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using UnityEngine.UI;

public class PredictionSelection : ISelectionStrategy
{
    private Gaussian _statistic;

    public PredictionSelection()
    {
        GetStatistic();
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

        Debug.Log(probs);
        var tmp = probs.ToList();
        target = objects[tmp.IndexOf(probs.Max())].gameObject;
    }

    private float GetProbability(GameObject obj)
    {
        if(!obj.TryGetComponent<SelectableObject>(out var selectable)) return 0;

        if(_statistic == null) GetStatistic();

        Vector2 pos = Utils.Utils.GetRelativePosition(obj, GameInstance.I.GazeManager.GazeVector, GameInstance.I.GazeManager.GazeOrigin);

        return _statistic.GetProbability(pos.x, pos.y);
    }

    private void GetStatistic()
    {
        _statistic = null;

        if(GameInstance.I == null) return;

        var manager = GameInstance.I.FindManager<FittsManager>();

        if(manager == null) return;

        _statistic = new Gaussian(manager.TargetWidth, manager.TargetAngle);
    }
}
