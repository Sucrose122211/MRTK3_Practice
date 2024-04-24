using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittsFactory
{
    float dist;
    float angle;
    float width;
    int totalNum;
    const string prefabName = "FittsTarget";

    public FittsFactory(float dist, float angle, float width, int totalNum)
    {
        this.dist = dist;
        this.angle = angle/2f;
        this.width = width/2f;
        this.totalNum = totalNum;
    }

    public GameObject GenerateTarget(int idx)
    {
        GameObject go = null;

        int hidx = idx/2;
        Vector3 pos = Quaternion.Euler(angle, 0, 0) * Vector3.forward;
        if(idx % 2 != 0) pos.y *= -1;
        pos = Quaternion.Euler(0, 0, hidx * 2 * Mathf.PI / totalNum * Mathf.Rad2Deg) * pos;

        go = Utils.Resource.Instantiate(prefabName);
        
        if(go == null) return null;

        go.transform.position = pos * dist;
        go.transform.localScale = dist * Mathf.Tan(width * Mathf.Deg2Rad) * go.transform.localScale;

        return go;
    }
}
