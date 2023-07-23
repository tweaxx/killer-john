using DG.Tweening;
using System;
using TweaxxGames.JamRaid;
using UnityEngine;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Transform black, maxter;
    [SerializeField] private float duration = 1f;
    [Space]
    [SerializeField] private float fadeInBlack = 10f;
    [SerializeField] private float fadeInMaxter = 1f;

    public void FadeIn(Action callback)
    {
        gameObject.SetActive(true);
        Utilities.DoActionDelayed(callback, duration);

        black.DOKill();
        maxter.DOKill();

        black.DOScale(fadeInBlack, duration);
        maxter.DOScale(fadeInMaxter, duration);
        maxter.DOLocalRotate(Vector3.forward * 360, duration, RotateMode.LocalAxisAdd);
    }

    public void FadeOut(Action callback = null)
    {
        gameObject.SetActive(true);
        Utilities.DoActionDelayed(() =>
        {
            gameObject.SetActive(false);
            callback?.Invoke();
        }, duration);

        black.DOKill();
        maxter.DOKill();

        black.DOScale(0, duration);
        maxter.DOScale(0, duration);
        maxter.DOLocalRotate(Vector3.forward * -360, duration, RotateMode.LocalAxisAdd);
    }
}
