using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BehaviourSingleton<GameManager>
{
    [SerializeField] GameObject m_plane;

    public GameObject Plane => m_plane;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInstance.I.UserType == EUSERTYPE.RECIEVER) return;
        
        m_plane = FindObjectOfType<GamePlane>().gameObject;
        GameInstance.I.FeedManager.GetFeedFactory(m_plane);
        GameInstance.I.GazeManager.GetGazeInteractor();
    }

    
}
