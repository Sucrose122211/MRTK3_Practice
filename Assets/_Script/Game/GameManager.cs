using Microsoft.MixedReality.Toolkit.MultiUse;
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
                GameInstance.I.AddManager(new FittsManager());
                break;
            default:
            break;
        }
    }
    
}