
// #define DEBUG

using Unity.Netcode;
using UnityEngine;

public enum EUSERTYPE{
    SENDER, RECIEVER
}

namespace Microsoft.MixedReality.Toolkit.MultiUse
{
    public class Initializer : NetworkBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField] EUSERTYPE userType;
        #elif STANDALONE_WIN
        EUSERTYPE userType = EUSERTYPE.RECIEVER;
        #else
        EUSERTYPE userType = EUSERTYPE.SENDER;
        #endif
        string m_SceneName;

        const string RecieverSceneName = "Server";
        const string SenderSceneName = "PickMan";

        [SerializeField] GameObject m_instancePrefab;
        [SerializeField] ProjectSceneManager m_sceneManager;

        private NetworkObject spawnObject;
        // Start is called before the first frame update
        void Start()
        {
            switch(userType)
            {        
                case EUSERTYPE.RECIEVER:
                    // SceneLoader.LoadScene(RecieverSceneName);

                    // m_sceneManager.SetSceneName(SenderSceneName);

                    NetworkManager.Singleton.OnServerStarted += OnServerStartedEvent;
                    NetworkManager.Singleton.OnClientConnectedCallback += OnConnectionEvent;
                    NetworkManager.Singleton.StartServer();
                    break;
                case EUSERTYPE.SENDER:
                    // SceneLoader.LoadScene(SenderSceneName);
                    NetworkManager.Singleton.StartClient();
                    var go = GameInstance.I;
                    if(go == null) break;

                    go.UserType = userType;
                    break;
                default:
                    return;
            }
        }

        void OnSceneEvent(SceneEvent sceneEvent)
        {
            switch (sceneEvent.SceneEventType)
            {
                case SceneEventType.LoadComplete:
                    {
                        break;
                    }
                case SceneEventType.UnloadComplete:
                    {
                        break;
                    }
                case SceneEventType.LoadEventCompleted:
                case SceneEventType.UnloadEventCompleted:
                    {
                        break;
                    }
            }
        }

        void OnConnectionEvent(ulong clientId)
        {
            if (clientId == NetworkManager.Singleton.LocalClientId)
            {
                Debug.Log("Server Connected");
            }
            else
            {
                Debug.Log($"Client {clientId} Connected");
                m_sceneManager.External_OnSceneEvent += OnSceneEvent;
                m_sceneManager.LoadScene(SenderSceneName);
            }
            
            spawnObject.Spawn();
        }

        void OnServerStartedEvent()
        {
            Debug.Log("Server Started");
            var go = Instantiate(m_instancePrefab);
            spawnObject = go.GetComponent<NetworkObject>();
            spawnObject.Spawn(false);

            GameInstance.I.UserType = userType;
        }
    }
}
