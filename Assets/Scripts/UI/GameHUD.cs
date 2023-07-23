using UnityEngine;
using UnityEngine.UI;

public class GameHUD : UIScreenBase
{
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSkip;
    [SerializeField] private TMPro.TextMeshProUGUI goalLeft;

    private void Start()
    {
        btnExit.onClick.AddListener(Exit);
        btnSkip.onClick.AddListener(Skip);
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

    private void Skip()
    {
        LevelManager.Instance.CompleteLevel(false);
    }
}
