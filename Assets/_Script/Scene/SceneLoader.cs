using System;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    static Scene _currentScene;
    public static Scene CurrentScene => _currentScene;
    public static void LoadScene(string sceneName, UnityAction<Scene, LoadSceneMode> OnSceneLoaded = null)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
        _currentScene = SceneManager.GetActiveScene();
    }

    public static void LoadScene(int sceneIndex, UnityAction<Scene, LoadSceneMode> OnSceneLoaded = null)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneIndex);
        _currentScene = SceneManager.GetActiveScene();
    }
}