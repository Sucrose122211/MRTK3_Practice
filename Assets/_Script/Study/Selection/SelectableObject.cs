using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public class SelectableObject : MonoBehaviour, ISelectable
{
    public virtual float Width {
        get{
            FittsManager manager;
            if(GameInstance.I == null || (manager = GameInstance.I.FindManager<FittsManager>()) == null) return transform.localScale.x;
            return 2 * Mathf.Atan(transform.localScale.x / manager.TargetDist) * Mathf.Rad2Deg;
        }
    }
}
