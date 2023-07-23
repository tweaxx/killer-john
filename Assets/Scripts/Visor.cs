using DG.Tweening;
using System;
using UnityEngine;

public class Visor : MonoBehaviour
{
    public event Action<GameObject> OnEnter;

    [SerializeField] private Color damageColor = Color.red;

    private float blinkDuration = 0.08f;
    private SpriteRenderer _renderer;
    private Color _initialColor;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _initialColor = _renderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(collision.gameObject);
    }

    public void BlinkAnimation()
    {
        _renderer.DOKill();
        _renderer.DOColor(damageColor, blinkDuration).OnComplete(() =>
        {
            _renderer.DOColor(_initialColor, blinkDuration);
        }).SetLoops(3, LoopType.Yoyo);
    }
}
