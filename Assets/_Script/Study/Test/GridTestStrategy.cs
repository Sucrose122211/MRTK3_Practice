

using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.VisualScripting;
using UnityEngine;

public class GridTestStrategy : MonoBehaviour, ITestStrategy
{
    ESelectionStrategy _selectionStrategy;
    GameInstance GI;

    int idx;
    bool isTest;
    float timer;
    int intendNum;
    GameObject currentTarget;
    Vector3 lastPinchPoint;

    public float TargetAngle => targetAngle;
    public float TargetWidth => targetWidth;
    public float TargetDist => dist;

    const float targetAngle = 17;       // A [degree]
    const float targetWidth = 1.4f;    // W [degree]
    const int targetNum = 22;           // Number of targets
    const float dist = 15;              // target distance

    private void Start()
    {
        _selectionStrategy = GameManager.I.SelectionStrategy;
        new SelectManager(_selectionStrategy);
        isTest = false;
        timer = 0;
        currentTarget = null;
        idx = 0;
        intendNum = 0;

        GI.PinchManager.External_OnPinchEvent += OnRightPinch;
    }

    private void Update() 
    {
        if(!isTest) return;

        if(idx == targetNum && currentTarget == null)
        {
            isTest = false;
            return;
        }

        currentTarget = 
        currentTarget != null ? currentTarget : GenerateTarget(idx++);

        timer += Time.deltaTime;
    }

    public void StartTest()
    {
        isTest = true;
    }

    private GameObject GenerateTarget(int idx)
    {
        return null;
    }

    private void OnRightPinch()
    {

    }

    private void OnLeftPinch()
    {

    }
}
