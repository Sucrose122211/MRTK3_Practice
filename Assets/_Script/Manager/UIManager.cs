using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;
using TMPro;

public class UIManager : ManagerBase
{
    private Canvas _canvas;
    private TextMeshProUGUI _scoreBoard;
    private GameInstance _GI;

    public UIManager(Canvas canvas)
    {
        _canvas = canvas;
        _scoreBoard = canvas.GetComponentInChildren<TextMeshProUGUI>();
        _GI = GameInstance.I;
    }

    public void UpdateScore()
    {
        _scoreBoard.text = $"Score: {_GI.Score}";
    }
}
