using UnityEngine;
using UnityEngine.UI;

public class GameHUD : UIScreenBase
{
    [SerializeField] private Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }

    private void Exit()
    {
        UIManager.Instance.FadeIn(() =>
        {
            SceneManager.Instance.LoadScene(SceneType.Menu);
        });
    }
}