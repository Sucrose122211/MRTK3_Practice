using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using data;
using System;

public partial class DataManager : NetworkBehaviour
{
    // TODO: JSON 형태로 data 송수신

    void Update()
    {
        if(!IsOwner) return;
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void RecieveDataServerRPC(string packet)
    {
        Debug.Log("recieved");
        _ = StartCoroutine(FetchData(packet));
    }

    public void SendDataToServer<T>(T data) where T : ManagableData
    {
        Debug.Log("Send RPC");
        RecieveDataServerRPC(data.GetPacket());
    }

    IEnumerator FetchData(string packet)
    {
        yield return null;
        Debug.Log("Recieved: " + packet);
        string[] datas = packet.Split(parsingDelimeter);

        Type dataType = Type.GetType(datas[0]);
        Debug.Log(dataType.Name);
        if(!typeof(ManagableData).IsAssignableFrom(dataType)) yield break;

        var constructor = dataType.GetConstructor(Type.EmptyTypes);
        var data = constructor.Invoke(null);

        AddData(data);
        Debug.Log($"Data - {data} Fetched");
    }

    public void TestSend()
    {
        var testdata = new TestData();
        testdata.Add("testKey", "testValue");
        SendDataToServer(testdata);
    }
}
