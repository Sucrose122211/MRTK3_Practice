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
    private IGazeInteractable currentTarget;

    public float MovedAngle => movedAngle;

    private GazeMode gazeMode;
    public GazeMode GazeMode => gazeMode;

    private const float threshold = 0.8f;
    private const float minTime = 0.5f;
    private const float stopHeadThreshold = 1;

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
    Vector3 startVector = Vector3.forward;
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

#region BimodalGaze
        movedAngle = Vector3.Angle(lastHeadVector, HeadVector);
        lastHeadVector = HeadVector;
        if(movedAngle < stopHeadThreshold){
            /* Head Stoped */
            timer = 0;
            startVector = HeadVector;
            gazeMode = GazeMode.EYE;
            return;
        }
        /*Head Moves*/
        timer += Time.deltaTime;
        if(timer > minTime && Vector3.Angle(startVector, HeadVector) > threshold)
        {
            gazeMode = GazeMode.HEAD;
        }
        else gazeMode = GazeMode.EYE;
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
