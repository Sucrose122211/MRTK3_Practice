using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using UnityEngine;
using Unity.Netcode;

namespace data{
    [System.Serializable]
    public abstract class ManagableData
    {
        public Dictionary<string, string> Datas;
        protected string name = "";
        public ManagableData(){
            Datas = new();
        }

        public void Add(string key, string value)
        {
            if(Datas.ContainsKey(key))
            {
                Datas[key] = value;
            }
            else
            {
                Datas.Add(key, value);
            }
        }

        public string GetPacket()
        {
            Pack();
            string result = name + "\n";
            foreach(KeyValuePair<string, string> kv in Datas)
            {
                result += kv.Key + ":" + kv.Value + '\n';
            }
            UnityEngine.Debug.Log(result);
            return result;
        }

        public abstract void Pack();    // Class data to DataDict

        public abstract void UnPack();  // DataDict to class data
    }
}