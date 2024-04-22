using System.Collections;
using System.Collections.Generic;
using data;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public class FittsManager : ManagerBase, IPinchInteractable
{
    int idx;
    bool isTest;
    float timer;
    GameObject currentTarget;
    GameInstance GI;
    FittsFactory factory;

    const float targetAngle = 17;       // A [degree]
    const float targetWidth = 1.75f;    // W [degree]
    const int targetNum = 22;           // Number of targets
    const float dist = 15;              // target distance

    public override void OnAwake()
    {
        base.OnAwake();

        isTest = false;
        GI = GameInstance.I;
        timer = 0;
        currentTarget = null;
        factory ??= new FittsFactory(
            dist, targetAngle, targetWidth, targetNum
        );
        idx = 0;
    }

    public void StartTest()
    {
        if(isTest) return;

        isTest = true;
        timer = 0;
        idx = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(!isTest) return;

        currentTarget =
        currentTarget != null ? currentTarget : factory.GenerateTarget(idx++);

        timer += Time.deltaTime;

        if(idx == targetNum) 
            isTest = false;
    }

    public void OnPinch()
    {
        if(!isTest || currentTarget == null || GI == null) return;

        float time = timer;
        timer = 0;
        Vector3 target = currentTarget.transform.position - GI.GazeManager.GazeOrigin;
        Vector3 normY = Vector3.Cross(target, Vector3.up);
        Vector3 normX = Vector3.Cross(normY, target);

        FittsData data = new FittsData
        {
            Time = time,
            x = Vector3.SignedAngle(target, GI.GazeManager.GazeVector, normX),
            y = Vector3.SignedAngle(target, GI.GazeManager.GazeVector, normY),
            Width = targetWidth,
            dist = dist
        };

        GI.SendDataRPC(data.GetPacket());
        Object.Destroy(currentTarget);
        currentTarget = null;
    }
}
