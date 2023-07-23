using System;
using UnityEngine;


public enum SceneType
{
    Menu,
    Level1,
    Level2,
    Level3,
}

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    public event Action<SceneType> OnSceneLoaded;

    public SceneType CurrentScene { get; private set; } = SceneType.Menu;
    public SceneType TargetScene { get; private set; } = SceneType.Menu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoadedHandler;

            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (Enum.TryParse(typeof(SceneType), scene.name, true, out var type))
            {
                CurrentScene = (SceneType)type;
                TargetScene = CurrentScene;

                Debug.Log($"CurrentScene = {CurrentScene}");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoadedHandler;

        if (Instance == this)
            Instance = null;
    }

    private void OnSceneLoadedHandler(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        CurrentScene = TargetScene;
        OnSceneLoaded?.Invoke(CurrentScene);

        if (!Application.isEditor)
            Cursor.lockState = CursorLockMode.Confined;

        switch (CurrentScene)
        {
            case SceneType.Menu:
                UIManager.Instance.Show<Menu>();
                SoundManager.Instance.PlayMusic(SoundType.MenuMusic);
                break;
            default:
                UIManager.Instance.Show<GameHUD>();
                LevelManager.Instance.StartLevel();
                SoundManager.Instance.PlayMusic(SoundType.GameplayMusic);
                break;
        }

        UIManager.Instance.FadeOut();
    }

    public void LoadScene(SceneType type)
    {
        TargetScene = type;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(type.ToString());
    }
}

