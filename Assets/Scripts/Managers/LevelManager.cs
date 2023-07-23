using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public event Action OnLevelComplete;
    public bool IsLevelComplete { get; private set; }
    public SceneType Level => level;

    [SerializeField] private SceneType level;
    [SerializeField] private bool isFinal;

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

        Debug.Log($"{Level} Started");
    }

    public void CompleteLevel()
    {
        Debug.Log($"{Level} Complete!!! Final: {isFinal}");

        IsLevelComplete = true;
        OnLevelComplete?.Invoke();

        if (isFinal)
        {
            UIManager.Instance.Show<VictoryScreen>();
        }
        else
        {
            UIManager.Instance.FadeIn(() =>
            {
                SceneManager.Instance.LoadScene((SceneType)(int)Level + 1);
            });
        }
    }

    public void Restart()
    {
        UIManager.Instance.Show<GameOver>();
    }

    public void CheckIfComplete()
    {
        var unitsLeft = UnitManager.Instance.Units.FindAll(v => v != null && v.needToKill && v.Health.IsAlive);

        Debug.Log($"unitsLeft: {unitsLeft.Count}");

        if (unitsLeft.Count <= 0)
            CompleteLevel();
    }
}

