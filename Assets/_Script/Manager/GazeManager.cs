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
            return _gazeInteractor.transform.forward;
        }
    }

    public Vector3 GazeOrigin
    {
        get {
            return _gazeInteractor.transform.position;
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();
        var go = GameObject.Find("MRTK Gaze Controller");

        _gazeInteractor = go.GetComponentInChildren<FuzzyGazeInteractor>();

        if(_gazeInteractor == null) return;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(_gazeInteractor == null) return;
    }
}
