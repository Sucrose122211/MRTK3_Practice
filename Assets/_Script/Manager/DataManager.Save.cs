using System.Collections;
using System.Collections.Generic;
using System.IO;
using data;
using Unity.Mathematics;
using UnityEngine;

public partial class DataManager : ManagerBase
{
    readonly string fileName = "Result.json";
    public void ExportJSON<T>() where T : ManagableData
    {
        if(!DataDict.ContainsKey(nameof(T))) { Debug.Log("No Such Data"); return; }

        var datas = DataDict[nameof(T)] as List<T>;

        string ToJsonData = nameof(T) + '\n';
        foreach(T data in datas)
        {
            ToJsonData += JsonUtility.ToJson(data) + '\n';
        }
        
        string filePath = Application.persistentDataPath + "/" + fileName;

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log(fileName + " saved");
    }

    public void ExportAllJSON()
    {
        string totalData = "";
        foreach(KeyValuePair<string, List<ManagableData>> kv in DataDict)
        {
            var datas = DataDict[kv.Key];

            string ToJsonData = kv.Key;
            foreach(ManagableData data in datas)
            {
                ToJsonData += JsonUtility.ToJson(data) + '\n';
            }

            totalData += ToJsonData + '\n';
        }
        string filePath = Application.persistentDataPath + "/" + fileName;

        File.WriteAllText(filePath, totalData);
        Debug.Log(fileName + " saved in " + filePath);
    }
}
