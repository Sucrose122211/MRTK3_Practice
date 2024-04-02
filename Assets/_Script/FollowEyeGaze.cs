using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.Input;
using UnityEngine;

public class FollowEyeGaze : MonoBehaviour
{
    // [SerializeField] private float EyeGazeDepth;
    private Vector3 _initPosition;
    // Start is called before the first frame update
    private GazeManager manager;
    void Start()
    {
        if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) gameObject.SetActive(false);
        _initPosition = FindAnyObjectByType<GamePlane>().transform.position;
        manager = GameInstance.I.GazeManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null) transform.position = _initPosition;

        if (Physics.Raycast(manager.GazeOrigin, manager.GazeVector, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Background")))
        {
            transform.position = hit.point;
            return;
        }

        transform.position = _initPosition;
    }
}
