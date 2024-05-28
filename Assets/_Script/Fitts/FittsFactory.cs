using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.Netcode;
using UnityEngine;

public class FittsFactory
{
    readonly float dist;
    readonly float angle;
    readonly float width;
    readonly int totalNum;
    readonly EFITTSTYPE type;
    readonly float scaleFactor;

    private GazeManager gazeManager;

    const string dataPrefabName = "FittsTarget";
    const string testPrefabName = "TestTarget";

    public FittsFactory(float dist, float angle, float width, int totalNum, EFITTSTYPE type)
    {
        this.dist = dist;
        this.angle = angle/2f;
        this.width = width/2f;
        this.totalNum = totalNum;
        this.type = type;

        scaleFactor = dist * Mathf.Tan(this.width * Mathf.Deg2Rad);
        gazeManager = GameInstance.I.GazeManager;
    }

    public GameObject GenerateTarget(int idx)
    {
        int hidx = idx/2;
        Vector3 pos = Quaternion.Euler(-angle, 0, 0) * Vector3.forward;
        if(idx % 2 != 0) pos.y *= -1;
        pos = Quaternion.Euler(0, 0, -hidx * 2 * Mathf.PI / totalNum * Mathf.Rad2Deg) * pos;

        string prefab = type switch
        {
            EFITTSTYPE.DATA => dataPrefabName,
            EFITTSTYPE.TEST => testPrefabName,
            _ => dataPrefabName
        };
        GameObject go = Utils.Resource.Instantiate(prefab);

        if (go == null) return null;

        float rot = hidx * 2 * Mathf.PI / totalNum + (idx%2 == 0 ? 0 : Mathf.PI);

        go.transform.position = gazeManager.GazeOrigin + pos * dist;

        var target = go.GetComponent<FittsTarget>();
        target.SetSize(scaleFactor);
        target.SetRotation(Quaternion.Euler(0, 0, -rot * Mathf.Rad2Deg));

        return go;
    }
}
