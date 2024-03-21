using UnityEngine;
using System.Collections;

public abstract class BehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T i = null;

    public static T I
    {
        get
        {
            if (i == null)
            {
                i = FindObjectOfType(typeof(T)) as T;
                if (i == null)
                {

                }
            }
            return i;
        }
        set
        {
            i = value;
        }
    }
}