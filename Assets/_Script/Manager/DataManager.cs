using System.Collections.Generic;
using data;
using UnityEngine;

public partial class DataManager : ManagerBase
{
    Dictionary<string, List<ManagableData>> DataDict;
    
    readonly char[] parsingDelimeter = {'\n'};

    // Start is called before the first frame update
    public override void OnStart()
    {
        DataDict = new();
    }

    public void AddData<T>(T data) where T : ManagableData
    {
        string key = typeof(T).FullName;
        if(DataDict.ContainsKey(key))
        {
            DataDict[key].Add(data);
        }
        else
        {
            DataDict.Add(key, new List<ManagableData>{ data });
        }
    }

    public void RemoveData<T>(T data) where T : ManagableData
    {
        if(!DataDict.ContainsKey(nameof(T))) return;

        DataDict[nameof(T)].Remove(data);
    }

    public void AddData(string dataName, ManagableData data)
    {
        string key = dataName;
        if(DataDict.ContainsKey(key))
        {
            DataDict[key].Add(data);
        }
        else
        {
            DataDict.Add(key, new List<ManagableData>{ data });
        }
    }

    public void RemoveData(string dataName, ManagableData data)
    {
        if(!DataDict.ContainsKey(dataName)) return;

        DataDict[dataName].Remove(data);
    }


    public List<ManagableData> GetData(string dataType)
    {
        if(DataDict.ContainsKey(dataType))
        {
            return DataDict[dataType];
        }
        return null;
    }

    public void ClearData()
    {
        Debug.Log("Data Cleared");
        DataDict.Clear();
    }
}
