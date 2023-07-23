using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    UIClickDefault = 0,
    UIClickNegative = 1,
    MenuMusic = 2,
    GameplayMusic = 3,
    PistolShoot = 4,
    ManScream = 5,
    Attention = 6,
    ManScreamStart = 7,
    BloodBurst = 8,
}

[System.Serializable]
public class SoundData
{
    public SoundType Type;
    public AudioClip Clip;
    [Range(0f, 1f)]
    public float Volume = 1;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public bool IsSoundOn { get; private set; } = true;
    public bool IsMusicOn { get; private set; } = true;

    public float Volume { get; private set; } = 1f;

    [Header("Settings")]
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Music")]
    [SerializeField] private List<SoundData> music = new List<SoundData>();
    [Header("Sounds")]
    [SerializeField] private List<SoundData> sounds = new List<SoundData>();

    private SoundData _currentMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Volume = PlayerPrefs.GetFloat("volume", 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        switch (SceneManager.Instance.CurrentScene)
        {
            case SceneType.Menu:
                PlayMusic(SoundType.MenuMusic);
                break;
            default:
                PlayMusic(SoundType.GameplayMusic);
                break;
        }
    }

    public void SetVolume(float value)
    {
        Volume = value;
        PlayerPrefs.SetFloat("volume", Volume);

        musicSource.volume = _currentMusic != null ? _currentMusic.Volume * Volume : Volume;
    }

    public void SetMusicState(bool isOn)
    {
        musicSource.mute = !isOn;
        IsMusicOn = isOn;
    }

    public void PlaySound(SoundType type)
    {
        var data = GetSound(type);

        //Debug.Log($"PlaySound: {type}, clip = {clip}");

        if (data != null && data.Clip != null)
        {
            oneShotSource.volume = Volume * data.Volume;
            oneShotSource.PlayOneShot(data.Clip);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            oneShotSource.PlayOneShot(clip);
        }
    }

    public void PlayMusic(SoundType type)
    {
        var data = GetMusic(type);
        if (data != null && data.Clip != null)
        {
            _currentMusic = data;
            musicSource.volume = Volume * data.Volume;
            musicSource.clip = data.Clip;
            musicSource.Play();
        }
    }

    public SoundData GetSound(SoundType type)
    {
        var list = sounds.FindAll(v => v.Type == type && v.Clip != null);
        if (list.Count > 0)
            return list[Random.Range(0, list.Count)];

        return null;
    }

    public SoundData GetMusic(SoundType type)
    {
        return music.Find(v => v.Type == type);
    }
}

