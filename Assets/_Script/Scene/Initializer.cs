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
        #elif UNITY_STANDALONE_WIN
        EUSERTYPE userType = EUSERTYPE.RECIEVER;
        #else
        EUSERTYPE userType = EUSERTYPE.SENDER;
        #endif

        public static EUSERTYPE UserType;
        private string SenderSceneName;

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
                spawnObject.Spawn();
            }
            else
            {
                Debug.Log($"Client {clientId} Connected");
                Debug.Log("Load Scene " + SenderSceneName);
                m_sceneManager.External_OnSceneEvent += OnSceneEvent;
            }
        }

        void OnDisconnectionEvent(ulong clientId)
        {
            if(NetworkManager.ConnectedClientsList.Count > 0) return;

            // m_sceneManager.LoadScene("Init");
        }

        void OnServerStartedEvent()
        {
            Debug.Log("Server Started");
            var go = Instantiate(m_instancePrefab);
            spawnObject = go.GetComponent<NetworkObject>();
            spawnObject.Spawn(false);
        }

        public void OpenScene(string name)
        {
            if(!NetworkManager.Singleton.IsConnectedClient) return;
            OpenSceneServerRPC(name);
        }

        [Rpc(SendTo.Server, RequireOwnership = false)]
        private void OpenSceneServerRPC(string name)
        {
            m_sceneManager.LoadScene(name);
        }
    }
}
