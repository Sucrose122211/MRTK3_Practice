using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using MixedReality.Toolkit.Input;
using UnityEngine;

enum ECURSORTYPE{
    PLANE, DIST
}

public class FollowEyeGaze : MonoBehaviour
{
    [SerializeField] private float cursorDepth;
    [SerializeField] ECURSORTYPE cursorType;
    private Vector3 _initPosition;
    // Start is called before the first frame update
    private GazeManager manager;
    void Start()
    {
        switch(cursorType){
            case ECURSORTYPE.PLANE:
                _initPosition = FindAnyObjectByType<GamePlane>().transform.position;
                break;
            case ECURSORTYPE.DIST:
                _initPosition = Vector3.zero;
                break;
            default:
                break;
        }
        
        manager = GameInstance.I.GazeManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null) transform.position = _initPosition;

        switch(cursorType){
            case ECURSORTYPE.PLANE:
                if (Physics.Raycast(manager.GazeOrigin, manager.GazeVector, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Background")))
                {
                    transform.position = hit.point;
                    // TODO: normal 값으로 회전
                    return;
                }
                break;
            case ECURSORTYPE.DIST:
                transform.position = manager.GazeOrigin + manager.GazeVector * cursorDepth;
                break;
        }

        transform.position = _initPosition;
    }
}
