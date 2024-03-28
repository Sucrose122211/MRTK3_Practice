using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EUSERTYPE{
    SENDER, RECIEVER
}

public class Initializer : NetworkBehaviour
{
    [SerializeField] EUSERTYPE userType;
    string m_SceneName;

    const string RecieverSceneName = "Server";
    const string SenderSceneName = "PickMan";
    // Start is called before the first frame update
    void Start()
    {
        switch(userType)
        {
            case EUSERTYPE.RECIEVER:
                // SceneLoader.LoadScene(RecieverSceneName);
                m_SceneName = RecieverSceneName;
                NetworkManager.Singleton.StartServer();
                break;
            case EUSERTYPE.SENDER:
                // SceneLoader.LoadScene(SenderSceneName);
                m_SceneName = SenderSceneName;
                NetworkManager.Singleton.StartClient();
                break;
            default:
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
