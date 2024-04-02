using System.Collections.Generic;
using data;

public partial class DataManager : ManagerBase
{
    Dictionary<string, List<ManagableData>> DataDict;
    
    readonly char[] parsingDelimeter = {' ', '\n', ':'};

    // Start is called before the first frame update
    public override void OnStart()
    {
        DataDict = new();
    }

    public void AddData<T>(T data)
    {
        if(data is not ManagableData) return;

        string key = typeof(TestData).FullName;
        if(DataDict.ContainsKey(key))
        {
            DataDict[key].Add(data as ManagableData);
        }
        else
        {
            DataDict.Add(key, new List<ManagableData>{ data as ManagableData });
        }
    }

    public void RemoveData<T>(T data)
    {
        if(data is not ManagableData || !DataDict.ContainsKey(nameof(T))) return;

        DataDict[nameof(T)].Remove(data as ManagableData);
    }

    public List<ManagableData> GetData(string dataType)
    {
        if(DataDict.ContainsKey(dataType))
        {
            return DataDict[dataType];
        }
        return null;
    }

    void ParseJSON()
    {
        
    }

    void Save()
    {

    }
}
