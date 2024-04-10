using MixedReality.Toolkit.Input;
using UnityEngine;
using MixedReality.Toolkit.Subsystems;

public enum GazeMode{
    HEAD, EYE
}



public class GazeManager: ManagerBase
{

    private FuzzyGazeInteractor _gazeInteractor;
    private Camera _head;
    private Vector3 lastHeadVector;
    private float movedAngle;

    public float MovedAngle => movedAngle;

    private GazeMode gazeMode;
    public GazeMode GazeMode => gazeMode;

    private const float threshold = 0.18f;

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
            if(_gazeInteractor == null) return Vector3.forward;
            return _gazeInteractor.rayOriginTransform.position;
        }
    }

    public Vector3 HeadOrigin
    {
        get {
            if(_head == null) return Vector3.zero;
            return _head.transform.position;
            
        }
    }

    public Vector3 HeadVector
    {
        get {
            if(_head == null) return Vector3.forward;
            return _head.transform.forward;
        }
    }

    public Vector3 RayOriginVector
    {
        get {
            if(movedAngle < threshold) return GazeOrigin;
            return HeadOrigin;
        }
    }

    public Vector3 RayDirectionVector
    {
        get{
            if(movedAngle < threshold) return GazeVector;
            return HeadVector;
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();

        _gazeInteractor = GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
        _head = GameObject.FindAnyObjectByType<Camera>();

        // if(_gazeInteractor == null || _head == null) return;
        lastHeadVector = HeadVector;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        // Debug.Log($"{lastHeadVector}, {HeadVector}");
        
        movedAngle = Vector3.Angle(lastHeadVector, HeadVector);
        lastHeadVector = HeadVector;

        if(movedAngle > threshold) gazeMode = GazeMode.HEAD;
        else gazeMode = GazeMode.EYE;
    }

    public override void OnSceneChange()
    {
        base.OnSceneChange();

        _gazeInteractor = _gazeInteractor != null ? _gazeInteractor : GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
        _head = _head != null ? _head : Camera.main;

        Debug.Log($"Camera: {_head}");
    }

    public void GetGazeInteractor()
    {
        _gazeInteractor = _gazeInteractor != null ? _gazeInteractor : GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
    }
}
