using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public event Action OnLevelComplete;
    public bool IsLevelComplete { get; private set; }
    public SceneType Level => level;

    [SerializeField] private SceneType level;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void StartLevel()
    {
        IsLevelComplete = false;

        Debug.Log($"Level {Level} Started");
    }

    public void CompleteLevel()
    {
        Debug.Log($"Level {Level} Complete!!!");

        IsLevelComplete = true;
        OnLevelComplete?.Invoke();
    }

    public void Restart()
    {
        SceneManager.Instance.LoadScene(Level);
    }

    public void CheckIfComplete()
    {
        var unitsLeft = UnitManager.Instance.Units.FindAll(v => v != null && v.needToKill && v.Health.IsAlive);

        if (unitsLeft.Count <= 0)
            CompleteLevel();
    }
}

