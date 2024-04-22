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
    private Vector3 lastGazeVector;
    private float headMovedAngle;
    private float gazeMovedAngle;
    private IGazeInteractable currentTarget;

    public float MovedAngle => headMovedAngle;

    private GazeMode gazeMode;
    public GazeMode GazeMode => gazeMode;

    private const float headGazeThreshold = 20f;
    private const float GazeOverThreshold = 10f;
    private const float naturalTimeThreshold = 0.15f;
    private const float stopHeadThreshold = 1.5f;
    private const float stopEyeThreshold = 16f;

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
            if(gazeMode == GazeMode.HEAD) return HeadOrigin;
            return GazeOrigin;
        }
    }

    public Vector3 RayDirectionVector
    {
        // Fix current gaze vector and apply head rotation
        get{
            if(gazeMode == GazeMode.HEAD) return HeadVector;
            return GazeVector;
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();

        _gazeInteractor = GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
        _head = GameObject.FindAnyObjectByType<Camera>();

        // if(_gazeInteractor == null || _head == null) return;
        lastHeadVector = HeadVector;
        lastGazeVector = GazeVector;
        currentTarget = null;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Physics.Raycast(RayOriginVector, RayDirectionVector, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("GazeInteractable"));

        if(hit.collider == null || !hit.collider.TryGetComponent<IGazeInteractable>(out var target)) return;

        currentTarget ??= target;

        if(!currentTarget.Equals(target))
        {
            currentTarget.ExitEyeGaze();
            currentTarget = target;
        }
        
        if(!target.IsGazeOn) target.EnterEyeGaze();
        else target.StayEyeGaze(); 
    
    }

    float timer = 0;
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

#region BimodalGaze
        headMovedAngle = Vector3.Angle(lastHeadVector, HeadVector);
        lastHeadVector = HeadVector;

        gazeMovedAngle = Vector3.Angle(lastGazeVector, GazeVector);
        lastGazeVector = GazeVector;

        bool isHeadMoved = headMovedAngle > stopHeadThreshold;
        bool isEyeMoved = gazeMovedAngle > stopEyeThreshold;

        switch(gazeMode)
        {
            case GazeMode.EYE:
                if(!isHeadMoved) break;

                timer += Time.deltaTime;
                if(timer < naturalTimeThreshold) break;

                float headGazeAngle = Vector3.Angle(HeadVector, GazeVector);
                if(headGazeAngle > headGazeThreshold) 
                    gazeMode = GazeMode.HEAD;
                timer = 0;
                break;

            case GazeMode.HEAD:
                if(isEyeMoved || Vector3.Angle(RayDirectionVector, GazeVector) > GazeOverThreshold) 
                    gazeMode = GazeMode.EYE;
                break;

        }
#endregion
    }

    public override void OnSceneChange()
    {
        base.OnSceneChange();

        _gazeInteractor = _gazeInteractor != null ? _gazeInteractor : GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
        _head = _head != null ? _head : Camera.main;
    }

    public void GetGazeInteractor()
    {
        _gazeInteractor = _gazeInteractor != null ? _gazeInteractor : GameObject.FindAnyObjectByType<FuzzyGazeInteractor>();
    }
}
