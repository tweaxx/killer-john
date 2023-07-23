using UnityEngine;
using UnityEngine.UI;

public class GameOver : UIScreenBase
{
    [SerializeField] private Button btnPlay;

    private void Start()
    {
        btnPlay.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        UIManager.Instance.FadeIn(() =>
        {
            SceneManager.Instance.LoadScene(LevelManager.Instance.Level);
        });
    }
}
