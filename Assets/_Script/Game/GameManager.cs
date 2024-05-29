using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.VisualScripting;
using UnityEngine;

public enum GameType{
    PICKMAN, BIMODAL, FITTS, FITTSTEST, GRID
}

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] private GameType gameType;
    [SerializeField] private ESelectionStrategy selectionStrategy;
    ITestStratege test;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) return;

        // selectionStrategy = GameInstance.I.SelectionStrategy;
        // Debug.Log("Strategy: " + selectionStrategy);

        switch(gameType)
        {
            case GameType.PICKMAN:
                new FeedManager();
                break;
            case GameType.BIMODAL:
                break;
            case GameType.FITTS: 
            case GameType.FITTSTEST:
                test = new FittsTestStrategy(selectionStrategy, gameType);
                test.StartTest();
                break;
            case GameType.GRID:
                test = new GridTestStrategy();
                test.StartTest();
                break;
            default:
            break;
        }
    }
    
}