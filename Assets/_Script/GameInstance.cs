using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInstance : BehaviourSingleton<GameInstance>
{
    private FeedManager m_FeedManager;

    public FeedManager FeedManager => m_FeedManager;

    private GazeManager m_GazeManager;

    public GazeManager GazeManager => m_GazeManager;

    // private UIManager m_UIManager;
    // public UIManager UIManager => m_UIManager;


    [SerializeField] GameObject m_plane;

    public GameObject Plane => m_plane;
    private int _score;

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

        AwakeMangers();
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
}
