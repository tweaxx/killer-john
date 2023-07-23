using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public List<UIScreenBase> screens = new List<UIScreenBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            screens = GetComponentsInChildren<UIScreenBase>(true).ToList();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void FadeIn(Action callback)
    {
        callback?.Invoke();
    }

    public void FadeOut(Action callback = null)
    {
        callback?.Invoke();
    }

    public void Show<T>() where T : UIScreenBase
    {
        foreach (UIScreenBase screen in screens)
        {
            if (screen is T)
                screen.Show();
            else
                screen.Hide();
        }
    }
}
