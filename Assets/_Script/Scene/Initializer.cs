using Unity.Netcode;
using UnityEditor.MPE;
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
        #elif UNITY_STANDALONE_WIN
        EUSERTYPE userType = EUSERTYPE.RECIEVER;
        #else
        EUSERTYPE userType = EUSERTYPE.SENDER;
        #endif

        public static EUSERTYPE UserType;
        string m_SceneName;

        const string RecieverSceneName = "Server";
        const string SenderSceneName = "PickMan";

        [SerializeField] GameObject m_instancePrefab;
        [SerializeField] ProjectSceneManager m_sceneManager;

        private NetworkObject spawnObject;
        // Start is called before the first frame update
        void Awake()
        {
            UserType = userType;
        }
        void Start()
        {
            switch(userType)
            {        
                case EUSERTYPE.RECIEVER:
                    // SceneLoader.LoadScene(RecieverSceneName);

                    // m_sceneManager.SetSceneName(SenderSceneName);

                    NetworkManager.Singleton.OnServerStarted += OnServerStartedEvent;
                    NetworkManager.Singleton.OnClientConnectedCallback += OnConnectionEvent;
                    NetworkManager.Singleton.OnClientDisconnectCallback += OnDisconnectionEvent;
                    NetworkManager.Singleton.StartServer();
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
                        GameInstance.I.OnSceneChange();
                        GameInstance.I.SendEventRPC();
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

        void OnDisconnectionEvent(ulong clientId)
        {
            if(NetworkManager.ConnectedClientsList.Count > 0) return;

            m_sceneManager.LoadScene("Init");
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
