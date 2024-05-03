using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.VisualScripting;
using UnityEngine;

public enum GameType{
    PICKMAN, BIMODAL, FITTS, FITTSTEST
}

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] private GameType gameType;
    [SerializeField] private ESelcectionStrategy selcectionStrategy;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) return;

        switch(gameType)
        {
            case GameType.PICKMAN:
                new FeedManager();
                break;
            case GameType.BIMODAL:
                break;
            case GameType.FITTS:
                if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) break;
                var fmanager = new FittsManager(EFITTSTYPE.DATA);
                fmanager.StartTest();
                break;
            case GameType.FITTSTEST:                
                if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) break;
                var fManager = new FittsManager(EFITTSTYPE.TEST);
                new SelectManager(selcectionStrategy);
                fManager.StartTest();
                break;
            default:
            break;
        }
    }
    
}