using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using TMPro;

public class UIManager : ManagerBase
{
    // private Canvas _canvas;
    private Camera _camera;
    private GameInstance _GI;

    public Camera Camera => _camera;

    public override void OnAwake()
    {
        base.OnAwake();
        OnSceneChange();

        // _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _GI = GameInstance.I;
    }

    public override void OnSceneChange()
    {
        base.OnSceneChange();

        _camera = GameObject.FindAnyObjectByType<Camera>();
        var canvases = GameObject.FindObjectsOfType<Canvas>();

        foreach(Canvas canvas in canvases) canvas.worldCamera = _camera;
    }
}
