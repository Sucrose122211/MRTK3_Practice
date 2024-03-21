using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEyeGaze : MonoBehaviour
{
    // [SerializeField] private float EyeGazeDepth;
    private Vector3 _initPosition;
    // Start is called before the first frame update
    void Start()
    {
        _initPosition = GameInstance.I.Plane.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var manager = GameInstance.I.GazeManager;
        if(manager == null) transform.position = _initPosition;
        RaycastHit hit;

        if(Physics.Raycast(manager.GazeOrigin, manager.GazeVector, out hit, Mathf.Infinity, LayerMask.GetMask("Background")))
        {
            transform.position = hit.point;
            return;
        }

        transform.position = _initPosition;
    }
}
