using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Text;

public enum ENETWORKTYPE{
    CLIENT, SERVER, HOST,
} 

public class NetworkButton : MonoBehaviour
{
    public ENETWORKTYPE NetworkType;
    public NetworkManager NetworkManager;
    // Start is called before the first frame update
    void Start()
    {
        Button Button = GetComponent<Button>();
        switch(NetworkType)
        {
            case ENETWORKTYPE.CLIENT:
                Button.onClick.AddListener(OpenAsClient);
                return;
            case ENETWORKTYPE.SERVER:
                Button.onClick.AddListener(OpenAsServer);
                return;
            case ENETWORKTYPE.HOST:
                Button.onClick.AddListener(OpenAsHost);
                return;
            default:
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenAsServer()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(PlayerPrefs.GetString("name"));
    }

    void OpenAsClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    void OpenAsHost()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log(NetworkManager.Singleton.NetworkConfig.GetConfig());
    }
}
