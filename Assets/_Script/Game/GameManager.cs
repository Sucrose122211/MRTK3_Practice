using Microsoft.MixedReality.Toolkit.MultiUse;
using UnityEngine;

public enum GameType{
    PICKMAN, BIMODAL
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
            break;
            case GameType.BIMODAL:
                GameInstance.I.RemoveManager(GameInstance.I.FeedManager);
            break;
            default:
            break;
        }
    }

    
}