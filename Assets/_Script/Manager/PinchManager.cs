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
    private HandsAggregatorSubsystem aggregator = null;
    private bool toglePinch = false;
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

        if (!aggregator.TryGetPinchProgress(XRNode.RightHand, out bool ready, out bool isPinch, out float pinchAmount)) return;

        if(pinchAmount > 0.8f && !toglePinch)
        {
            toglePinch = true;
            Debug.Log("Pinch");
            m_OnPinchEvent?.Invoke();
            var interactables = GameInstance.I.GetAllInteractable<IPinchInteractable>();

            if(interactables.Length == 0) return;

            foreach(IPinchInteractable interactable in interactables)
                interactable.OnPinch();
        }
        if(pinchAmount < 0.8f && toglePinch)
        {
            toglePinch = false;
        }
    }

    public override void OnSceneChange()
    {
        base.OnSceneChange();

        GameInstance.I.CoroutineHelp(EnableWhenSubsystemAvailable());
    }
}
