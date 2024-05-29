using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using TMPro;
using UnityEngine;

public class UIToggleText : MonoBehaviour
{
    TextMeshPro text;
    [SerializeField] Initializer initializer;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = initializer.SelectionStrategy.ToString();
    }

    public void ToggleText()
    {
        text.text = initializer.SelectionStrategy.ToString();
    }
}
