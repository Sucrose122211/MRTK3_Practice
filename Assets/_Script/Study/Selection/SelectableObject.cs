using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableObject : MonoBehaviour, ISelectable
{
    public float Width {
        get{
            return transform.localScale.x;
        }
    }
}
