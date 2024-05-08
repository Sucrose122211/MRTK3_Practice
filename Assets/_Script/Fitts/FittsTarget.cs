using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittsTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SetSize(float size)
    {
        gameObject.transform.localScale = size * new Vector3(1,1,1);
    }
}
