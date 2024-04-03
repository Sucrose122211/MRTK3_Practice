using System;
using MixedReality.Toolkit.Input;
using Unity.VisualScripting;
using UnityEngine;

public enum GazeDirection
{
    CENTER, RIGHT, LEFT, UP, DOWN,
}

public class GazeManager: ManagerBase
{

    private FuzzyGazeInteractor _gazeInteractor;

    public Vector3 GazeVector
    {
        get {
            if(_gazeInteractor == null) return Vector3.zero;
            return _gazeInteractor.rayOriginTransform.forward;
        }
    }

    public Vector3 GazeOrigin
    {
        get {
            if(_gazeInteractor == null) return Vector3.zero;
            return _gazeInteractor.rayOriginTransform.position;
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();
        var go = GameObject.Find("MRTK Gaze Controller");

        if(go == null) return;

        _gazeInteractor = go.GetComponentInChildren<FuzzyGazeInteractor>();

        if(_gazeInteractor == null) return;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(_gazeInteractor == null) return;
    }

    public void GetGazeInteractor()
    {
        _gazeInteractor = _gazeInteractor != null ? _gazeInteractor : GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
    }
}
