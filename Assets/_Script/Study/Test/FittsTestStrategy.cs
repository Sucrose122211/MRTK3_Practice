using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FittsTestStrategy : ITestStrategy
{
    ESelectionStrategy _selectionStrategy;
    EFITTSTYPE _fittsType;
    GameType _gameType;

    public FittsTestStrategy(
        ESelectionStrategy selectionStrategy, 
        GameType gameType
    )
    {
        _selectionStrategy = selectionStrategy;
        _gameType = gameType;
        _fittsType = _gameType switch
        {
            GameType.FITTS => EFITTSTYPE.DATA,
            GameType.FITTSTEST => EFITTSTYPE.TEST,
            _ => EFITTSTYPE.DATA,
        };
    }

    public void StartTest()
    {
        var fmanager = new FittsManager(_fittsType);
        new SelectManager(_selectionStrategy);
        fmanager.StartTest();
    }
}
