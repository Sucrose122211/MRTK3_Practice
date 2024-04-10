using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using TMPro;

public class UIManager : ManagerBase
{
    private Canvas _canvas;
    private Camera _camera;
    private GameInstance _GI;

    public Camera Camera => _camera;

    public override void OnAwake()
    {
        base.OnAwake();
        _canvas = GameObject.FindAnyObjectByType<Canvas>();
        _camera = GameObject.FindAnyObjectByType<Camera>();

        // _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = _camera;
        _GI = GameInstance.I;
    }

    public override void OnSceneChange()
    {
        base.OnSceneChange();

        _camera = GameObject.FindAnyObjectByType<Camera>();
        _canvas.worldCamera = _camera;
    }
}
