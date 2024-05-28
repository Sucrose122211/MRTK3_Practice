using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Subsystems;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR;

public class PinchManager : ManagerBase
{
    private const float pinchThreshold = 0.95f;
    private const float unpinchThreshold = 0.5f;
    private HandsAggregatorSubsystem aggregator = null;
    private bool togleRightPinch = false;
    private bool togleLeftPinch = false;
    public delegate void OnPinchEvent();
    private OnPinchEvent m_OnPinchEvent;

    public event OnPinchEvent External_OnPinchEvent
    {
        add
        {
            m_OnPinchEvent += value;
        }
        remove
        {
            m_OnPinchEvent -= value;
        }
    }

    public override void OnAwake()
    {
        base.OnAwake();
        Debug.Log("PinchManager");
        Debug.Log(NetworkManager.Singleton.IsServer);
        if(!NetworkManager.Singleton.IsServer)
            GameInstance.I.CoroutineHelp(EnableWhenSubsystemAvailable());
    }

    // Wait until an aggregator is available.
    IEnumerator EnableWhenSubsystemAvailable()
    {
        yield return new WaitUntil(() => XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>() != null);
        aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
        Debug.Log(aggregator);
    }

    // TOOD: 왼쪽, 오른쪽 핀치 분리
    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if(aggregator == null || GameInstance.I.IsServer) return;

        CheckRightPinch();
        CheckLeftPinch();
    }

    private void CheckRightPinch()
    {
        if (!aggregator.TryGetPinchProgress(XRNode.RightHand, out bool ready, out bool isPinch, out float pinchAmount)) return;

        if(pinchAmount > pinchThreshold && !togleRightPinch)
        {
            togleRightPinch = true;
            Debug.Log("Right Pinch");
            m_OnPinchEvent?.Invoke();
            var interactables = GameInstance.I.GetAllInteractable<IPinchInteractable>();

            if(interactables.Length == 0) return;

            foreach(IPinchInteractable interactable in interactables)
                interactable.OnRightPinch();
        }
        if(pinchAmount < unpinchThreshold && togleRightPinch)
        {
            togleRightPinch = false;
        }
    }   

    private void CheckLeftPinch()
    {
        if (!aggregator.TryGetPinchProgress(XRNode.LeftHand, out bool ready, out bool isPinch, out float pinchAmount)) return;

        if(pinchAmount > pinchThreshold && !togleLeftPinch)
        {
            togleLeftPinch = true;
            Debug.Log("Left Pinch");
            m_OnPinchEvent?.Invoke();
            var interactables = GameInstance.I.GetAllInteractable<IPinchInteractable>();

            if(interactables.Length == 0) return;

            foreach(IPinchInteractable interactable in interactables)
                interactable.OnLeftPinch();
        }
        if(pinchAmount < unpinchThreshold && togleLeftPinch)
        {
            togleLeftPinch = false;
        }
    }   


    public override void OnSceneChange()
    {
        base.OnSceneChange();

        GameInstance.I.CoroutineHelp(EnableWhenSubsystemAvailable());
    }
}
