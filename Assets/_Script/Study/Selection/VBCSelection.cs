using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public class VBCSelection : ISelectionStrategy
{
    public void OnSelect(out GameObject target)
    {
        target = null;
        GameInstance GI = GameInstance.I;
        if(GI == null || GI.GazeManager == null) return;
        Debug.DrawRay(GI.GazeManager.GazeOrigin, GI.GazeManager.GazeVector * Mathf.Infinity, Color.green);
        if (!Physics.Raycast(GI.GazeManager.GazeOrigin, GI.GazeManager.GazeVector, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Selectable")))
            return;

        if(hit.normal.sqrMagnitude < 0) return;

        target = hit.collider.gameObject;
    }
}
