using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public class SquareSelectableObject : TestSelectObject
{

    /* Outter Circle */
    public override float Width {
        get{
            FittsManager manager;
            if(GameInstance.I == null || (manager = GameInstance.I.FindManager<FittsManager>()) == null) return transform.localScale.x;
            return 2 * Mathf.Atan(transform.localScale.x * Mathf.Sqrt(2) / manager.TargetDist) * Mathf.Rad2Deg;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
