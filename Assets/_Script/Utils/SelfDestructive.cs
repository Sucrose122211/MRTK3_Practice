using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructive : MonoBehaviour
{
    protected Func<bool> DestroyCondition;

    public virtual void Update()
    {
        if(DestroyCondition()) Destroy(this.gameObject);
    }
}
