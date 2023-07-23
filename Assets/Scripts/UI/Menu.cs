using UnityEngine;
using UnityEngine.UI;

public class Menu : UIScreenBase
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        soundSlider.value = SoundManager.Instance.Volume;

        btnPlay.onClick.AddListener(StartGame);
        soundSlider.onValueChanged.AddListener(SliderChanged);
    }

    private void SliderChanged(float volume)
    {
        SoundManager.Instance.SetVolume(volume);
    }

    private void StartGame()
    {
        UIManager.Instance.FadeIn(() =>
        {
            SceneManager.Instance.LoadScene(SceneType.Level1);
        });
    }
}
