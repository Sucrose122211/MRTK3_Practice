using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESelectionStrategy{
    VBC, PREDICTION,
}

public class SelectManager: ManagerBase
{
    private ISelectionStrategy _stragtegy;
    public SelectManager(ESelectionStrategy strategy): base()
    {
        SetStrategy(strategy);
    }

    public void SetStrategy(ESelectionStrategy strategy)
    {
        _stragtegy = strategy switch
        {
            ESelectionStrategy.VBC => new VBCSelection(),
            ESelectionStrategy.PREDICTION => new PredictionSelection(),
            _ => null,
        };
    }

    public void OnSelect(out GameObject target)
    {
        _stragtegy.OnSelect(out target);
    }
}
