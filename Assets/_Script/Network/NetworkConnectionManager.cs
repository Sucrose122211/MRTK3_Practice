using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.Netcode;
using UnityEngine;

public class NetworkConnectionManager : MonoBehaviour
{
    public void StartClient()
    {
        // SceneLoader.LoadScene(SenderSceneName);
        NetworkManager.Singleton.StartClient();

        return;
    }
}
