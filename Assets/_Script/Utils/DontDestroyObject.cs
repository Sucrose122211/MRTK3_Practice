using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var obj = FindObjectsOfType<DontDestroyObject>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
