using Microsoft.MixedReality.Toolkit.MultiUse;
using Unity.VisualScripting;
using UnityEngine;

public enum GameType{
    PICKMAN, BIMODAL, FITTS
}

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] private GameType gameType;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) return;

        switch(gameType)
        {
            case GameType.PICKMAN:
                GameInstance.I.AddManager(new FeedManager());
                break;
            case GameType.BIMODAL:
                break;
            case GameType.FITTS:
                if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) break;
                GameInstance.I.AddManager(new FittsManager());
                GameInstance.I.FindManager<FittsManager>().StartTest();
                break;
            default:
            break;
        }
    }
    
}