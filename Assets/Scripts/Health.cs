using DG.Tweening;
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDied;
    public event Action OnDamaged;

    public bool IsAlive => health > 0 && gameObject.activeInHierarchy;
    public bool IsDead => health <= 0 || !gameObject.activeInHierarchy;

    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private Color damageColor;
    [Space]
    [SerializeField] private GameObject deathVfx;
    [SerializeField] private float destroyAfter = 1f;
    
    private float blinkDuration = 0.08f;
    private SpriteRenderer _renderer;
    private Color _initialColor;

    private void Awake()
    {
        health = maxHealth;
        _renderer = GetComponent<SpriteRenderer>();
        _initialColor = _renderer.color;
    }

    private void OnDestroy()
    {
        _renderer.DOKill();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage(100);
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        //Debug.Log($"{name} takes {damage} damage!");

        health = Mathf.Max(health - damage, 0);
        OnDamaged?.Invoke();

        BlinkAnimation();

        if (IsDead)
        {
            //Debug.Log($"{name} died!");
            OnDied?.Invoke();

            if (deathVfx != null)
            {
                var vfx = Instantiate(deathVfx, new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity);
                Destroy(vfx, destroyAfter);

                SoundManager.Instance.PlaySound(SoundType.BloodBurst);
            }
        }
    }

    public void Restore()
    {
        health = maxHealth;
    }

    private void BlinkAnimation()
    {
        _renderer.DOKill();
        _renderer.DOColor(damageColor, blinkDuration).OnComplete(() =>
        {
            _renderer.DOColor(_initialColor, blinkDuration);
        }).SetLoops(3, LoopType.Yoyo);
    }

    public void SetVisibility(bool visible)
    {
        var color = _initialColor;
        color.a = 0;

        _renderer.DOKill();
        _renderer.color = visible ? _initialColor : color;
    }
}
