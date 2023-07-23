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

    [Header("Settings")]
    [SerializeField] private AudioSource oneShotSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Music")]
    [SerializeField] private List<SoundData> music = new List<SoundData>();
    [Header("Sounds")]
    [SerializeField] private List<SoundData> sounds = new List<SoundData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public void SetMusicState(bool isOn)
    {
        musicSource.mute = !isOn;
        IsMusicOn = isOn;
    }

    public void PlaySound(SoundType type)
    {
        var clip = GetSound(type);

        //Debug.Log($"PlaySound: {type}, clip = {clip}");

        if (clip != null)
        {
            oneShotSource.PlayOneShot(clip);
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
        var clip = GetMusic(type);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public AudioClip GetSound(SoundType type)
    {
        var list = sounds.FindAll(v => v.Type == type && v.Clip != null);
        if (list.Count > 0)
            return list[Random.Range(0, list.Count)].Clip;

        return null;
    }

    public AudioClip GetMusic(SoundType type)
    {
        return music.Find(v => v.Type == type)?.Clip;
    }
}

