using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.Input;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public partial class GameInstance : NetworkBehaviour
{
    private FeedManager m_FeedManager;

    public FeedManager FeedManager => m_FeedManager;

    private GazeManager m_GazeManager;

    public GazeManager GazeManager => m_GazeManager;

    private DataManager m_DataManager;

    public DataManager DataManager => m_DataManager;

    private ProjectSceneManager m_SceneManager;
    public ProjectSceneManager SceneManager => m_SceneManager;

    // private UIManager m_UIManager;
    // public UIManager UIManager => m_UIManager;

    private int _score;

    public EUSERTYPE UserType;

    public int Score{
        get{ return _score; }
        set{ 
            _score = value; 
            // m_UIManager?.UpdateScore();
        }
    }

    private List<ManagerBase> managers;

     void Awake() {
        managers = new();

        m_GazeManager = new GazeManager();
        m_FeedManager = new FeedManager();
        m_DataManager = new DataManager();

        m_SceneManager = GetComponent<ProjectSceneManager>();

        AwakeMangers();
        StartCoroutine(nameof(LoadingCoroutine));
    }

    void Start() {
        foreach(ManagerBase manager in managers)
        {
            manager.OnStart();
        }
    }

    void Update() {
        foreach(ManagerBase manager in managers)
        {
            manager.OnUpdate();
        }
    }

    void AwakeMangers()
    {
        foreach(ManagerBase manager in managers)
        {
            manager.OnAwake();
        }
    }

    IEnumerator LoadingCoroutine()
    {
        yield break;
    }

    private void OnDrawGizmos() {
        if(m_GazeManager == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(m_GazeManager.GazeOrigin, m_GazeManager.GazeVector);
    }

    public void CoroutineHelp(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void AddManager(ManagerBase manager)
    {
        managers ??= new();

        if(managers.Contains(manager)) return;

        managers.Add(manager);
    }

    public void RemoveManager(ManagerBase manager)
    {
        if(managers == null || !managers.Contains(manager)) return;
        
        managers.Remove(manager);
    }

    #region Singleton
    private static GameInstance i = null;

    public static GameInstance I
    {
        get
        {
            if (i == null)
            {
                i = FindObjectOfType(typeof(GameInstance)) as GameInstance;
                if (i == null)
                {

                }
            }
            return i;
        }
        set
        {
            i = value;
        }
    }
    #endregion
}