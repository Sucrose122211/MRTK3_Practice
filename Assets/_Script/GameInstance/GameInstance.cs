using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using MixedReality.Toolkit.Subsystems;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.MultiUse
{
    public partial class GameInstance : NetworkBehaviour
    {
        private GazeManager m_GazeManager;

        public GazeManager GazeManager => m_GazeManager;

        private DataManager m_DataManager;

        public DataManager DataManager => m_DataManager;

        private ProjectSceneManager m_SceneManager;
        public ProjectSceneManager SceneManager => m_SceneManager;

        private UIManager m_UIManager;
        public UIManager UIManager => m_UIManager;
        private PinchManager m_PinchManager;
        public PinchManager PinchManager => m_PinchManager;

        private int _score;

        public EUSERTYPE UserType => NetworkManager.Singleton.IsServer ? EUSERTYPE.RECIEVER : EUSERTYPE.SENDER;

        public int Score{
            get{ return _score; }
            set{ 
                _score = value; 
                // m_UIManager?.UpdateScore();
            }
        }

        private List<ManagerBase> managers;

#region ManagerRoutine
        void Awake() {
            managers = new();

            m_GazeManager = new GazeManager();
            m_DataManager = new DataManager();
            m_UIManager = new UIManager();
            m_PinchManager = new PinchManager();

            m_SceneManager = GetComponent<ProjectSceneManager>();

            // AwakeMangers();
        }

        void Start() {
            foreach(ManagerBase manager in managers)
            {
                manager.OnStart();
            }
            DontDestroyOnLoad(this);
        }

        void Update() {
            foreach(ManagerBase manager in managers)
            {
                manager.OnUpdate();
            }
        }

        void FixedUpdate() 
        {
            foreach(ManagerBase manager in managers)
            {
                manager.OnFixedUpdate();
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

        public void OnSceneChange()
        {
            foreach(ManagerBase manager in managers)
            {
                manager.OnSceneChange();
            }
        }
#endregion

#region Utils
        private void OnDrawGizmos() {
            if(m_GazeManager == null) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawRay(m_GazeManager.GazeOrigin, m_GazeManager.GazeVector * 22);
        }

        public void CoroutineHelp(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public ManagerBase AddManager(ManagerBase manager)
        {
            managers ??= new();

            if(!managers.Contains(manager))
            {
                managers.Add(manager);
                manager.OnAwake();
            }
            return managers.Find(x => x.Equals(manager));
        }

        public void RemoveManager(ManagerBase manager)
        {
            if(managers == null || !managers.Contains(manager)) return;
            
            managers.Remove(manager);
        }

        public T FindManager<T>() where T : ManagerBase
        {
            return (T)managers.Find(x => x is T);
        }
#endregion

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

#region Interactable
        public T[] GetAllInteractable<T>() where T : IInteractable
        {
            List<T> result = new();
            foreach(ManagerBase manager in managers)
            {
                if(manager is T interactable)
                {
                    result.Add(interactable);
                }
            }
            return result.ToArray();
        }
#endregion
    }
}
