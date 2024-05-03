using System.Collections;
using System.Collections.Generic;
using data;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using Utils;

public enum EFITTSTYPE{
    DATA, TEST
}

public class FittsManager : ManagerBase, IPinchInteractable
{
    int idx;
    bool isTest;
    float timer;
    GameObject currentTarget;
    GameInstance GI;
    FittsFactory factory;
    private readonly EFITTSTYPE fittsType;

    public float TargetAngle => targetAngle;
    public float TargetWidth => targetWidth;
    public float TargetDist => dist;

    const float targetAngle = 17;       // A [degree]
    const float targetWidth = 1.75f;    // W [degree]
    const int targetNum = 22;           // Number of targets
    const float dist = 15;              // target distance

    public FittsManager(EFITTSTYPE type) : base()
    {
        fittsType = type;
        factory ??= new FittsFactory(
            dist, targetAngle, targetWidth, targetNum, fittsType
        );
    }

    public override void OnAwake()
    {
        base.OnAwake();

        isTest = false;
        GI = GameInstance.I;
        timer = 0;
        currentTarget = null;
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

        if(idx == targetNum && currentTarget == null) isTest = false;

        currentTarget =
        currentTarget != null ? currentTarget : factory?.GenerateTarget(idx++);

        timer += Time.deltaTime;
    }

    public void OnPinch()
    {
        if(!isTest || currentTarget == null || GI == null) return;

        float time = timer;
        timer = 0;
        Vector2 RelPos = Utils.Utils.GetRelativePosition(currentTarget, GI.GazeManager.GazeVector, GI.GazeManager.GazeOrigin);

        FittsData data = new()
        {
            Time = time,
            x = RelPos.x,
            y = RelPos.y,
            Width = targetWidth,
            dist = dist
        };

        GI.SendDataRPC(data.GetPacket());

        GI.FindManager<SelectManager>().OnSelect(out var obj);
        var target = obj.GetComponent<TestSelectObject>();
        if(target != null && target.IsIntend) Debug.Log("Success");
        
        Object.Destroy(currentTarget);
        currentTarget = null;
    }
}
