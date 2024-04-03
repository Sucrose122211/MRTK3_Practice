using System.Collections;
using UnityEngine;
using data;
using System;
using Microsoft.MixedReality.Toolkit.MultiUse;

public partial class DataManager: ManagerBase
{
    // TODO: JSON 형태로 data 송수신
    public void SendDataToServer<T>(T data) where T : ManagableData
    {
        Debug.Log("Send RPC");
        GameInstance.I.SendDataRPC(data.GetPacket());
    }

    IEnumerator FetchDataCoroutine(string packet)
    {
        yield return null;
        Debug.Log("Recieved: " + packet);
        string[] datas = packet.Split(parsingDelimeter);

        Type dataType;

        try{
            dataType = Type.GetType(datas[0]);
        } catch(Exception e) {
            Debug.Log(e.Message + ": " + datas[0]);
            yield break;
        }

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

    public void FetchData(string packet)
    {
        GameInstance.I.CoroutineHelp(FetchDataCoroutine(packet));
    }
}
