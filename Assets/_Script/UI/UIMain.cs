using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using TMPro;
using UnityEngine;

public class UIMain : UIBase
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI TxtGazeMode;
    [SerializeField] private TextMeshProUGUI TxtHeadSpeed;
    [SerializeField] private float UIDist = 0.1f;

    void Awake()
    {
        TxtGazeMode.text = "";
        TxtHeadSpeed.text = "";
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        GameInstance GI = GameInstance.I;
        if(GI == null || GI.GazeManager == null) return;
        TxtGazeMode.text = "GazeMode: " + GI.GazeManager.GazeMode.ToString();
        TxtHeadSpeed.text = "HeadSpeed: " + GI.GazeManager.MovedAngle.ToString();
        var manager = GI.UIManager;
        if(manager == null || manager.Camera == null) return;

        transform.SetPositionAndRotation(GI.GazeManager.HeadOrigin + GI.GazeManager.HeadVector * UIDist,
                                         Quaternion.LookRotation(manager.Camera.transform.forward));
    }
}
