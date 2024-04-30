using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESelcectionStrategy{
    VBC, PREDICTION,
}

public class SelectManager: ManagerBase
{
    private ISelectionStrategy _stragtegy;
    public SelectManager(ESelcectionStrategy strategy)
    {
        SetStrategy(strategy);
    }

    public void SetStrategy(ESelcectionStrategy strategy)
    {
        _stragtegy = strategy switch
        {
            ESelcectionStrategy.VBC => new VBCSelection(),
            ESelcectionStrategy.PREDICTION => new PredictionSelection(),
            _ => null,
        };
    }

    public void OnSelect(out GameObject target)
    {
        _stragtegy.OnSelect(out target);
    }
}
