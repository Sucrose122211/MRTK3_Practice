using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.Input;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.MultiUse
{
    public partial class GameInstance: NetworkBehaviour
    {
        [Rpc(SendTo.Server, RequireOwnership = false)]
        private void RecieveDataServerRPC(string packet)
        {
            Debug.Log("recieved");
            m_DataManager.FetchData(packet);
        }

        public void SendDataRPC(string packet)
        {
            RecieveDataServerRPC(packet);
        }

    }
}
