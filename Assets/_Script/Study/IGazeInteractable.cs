using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public interface IGazeInteractable
{
    bool IsGazeOn { get; set; }

    void EnterEyeGaze()
    {
        IsGazeOn = true;
        OnEnter();
    }

    void ExitEyeGaze(){
        IsGazeOn = false;
        OnExit();
    }

    void StayEyeGaze(){
        if(!IsGazeOn) return;
        OnStay();
    }

    void OnEnter();

    void OnExit();

    void OnStay();
}
