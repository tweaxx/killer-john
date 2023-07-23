using DG.Tweening;
using System;
using TweaxxGames.JamRaid;
using UnityEngine;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField] private Transform black, maxter, clock;
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
        clock.DOKill();

        clock.DOScale(1, duration/2);
        black.DOScale(fadeInBlack, duration);
        maxter.DOScale(fadeInMaxter, duration);
        maxter.DOLocalRotate(Vector3.forward * 360, duration, RotateMode.LocalAxisAdd);

        SoundManager.Instance.PlaySound(SoundType.Clock);
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
        clock.DOKill();

        clock.DOScale(0, duration);
        black.DOScale(0, duration);
        maxter.DOScale(0, duration);
        maxter.DOLocalRotate(Vector3.forward * -360, duration, RotateMode.LocalAxisAdd);
    }
}
