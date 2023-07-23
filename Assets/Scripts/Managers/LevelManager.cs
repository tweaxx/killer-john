using System;
using TweaxxGames.JamRaid;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public event Action<int> OnUnitsLeft;

    public static LevelManager Instance { get; private set; }

    public event Action OnLevelComplete;
    public bool IsLevelComplete { get; private set; }
    public SceneType Level => level;
    public int UnitsLeft { get; private set; }

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

        Utilities.DoActionDelayed(CheckIfComplete, 0.5f);
    }

    public void CompleteLevel(bool wait = true)
    {
        if (SceneManager.Instance.IsLoading)
            return;

        Debug.Log($"{Level} Complete!!! Final: {isFinal}");

        IsLevelComplete = true;
        OnLevelComplete?.Invoke();

        if (isFinal)
        {
            UIManager.Instance.Show<VictoryScreen>();
        }
        else
        {
            Utilities.DoActionDelayed(() =>
            {
                UIManager.Instance.FadeIn(() =>
                {
                    SceneManager.Instance.LoadScene((SceneType)(int)Level + 1);
                });

            }, wait ? 1f : 0);
            
        }
    }

    public void Restart()
    {
        UIManager.Instance.Show<GameOver>();
    }

    public void CheckIfComplete()
    {
        var unitsLeft = UnitManager.Instance.Units.FindAll(v => v != null && v.needToKill && v.Health.IsAlive);
        UnitsLeft = unitsLeft.Count;

        OnUnitsLeft?.Invoke(unitsLeft.Count);

        if (unitsLeft.Count <= 0)
            CompleteLevel();
    }
}

