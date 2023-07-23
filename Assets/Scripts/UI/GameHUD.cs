using UnityEngine;
using UnityEngine.UI;

public class GameHUD : UIScreenBase
{
    [SerializeField] private Button btnExit;
    [SerializeField] private TMPro.TextMeshProUGUI goalLeft;

    private void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (LevelManager.Instance != null)
        {
            OnUnitsLeft(LevelManager.Instance.UnitsLeft);
        }
    }

    private void OnUnitsLeft(int value)
    {
        goalLeft.text = $"осталось: {value}";
    }

    private void Exit()
    {
        UIManager.Instance.FadeIn(() =>
        {
            SceneManager.Instance.LoadScene(SceneType.Menu);
        });
    }
}
