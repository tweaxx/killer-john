using System;
using UnityEngine;
using DG.Tweening;
using TweaxxGames.JamRaid;

public class Unit : MonoBehaviour
{
    public static event Action<Unit> OnCreated;
    public static event Action<Unit> OnDestroyed;

    [field: SerializeField]
    public bool IsVisible { get; private set; } = true;
    [field: SerializeField]
    public bool IsProvoked { get; private set; } = false;

    public Health Health { get; private set; }
    public MovementBase Movement { get; private set; }

    public bool needToKill;
    public bool isMyPlayer;

    private Sequence _provoke;
    private Unit _provokator;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        Health.OnDamaged += OnDamaged;
        Health.OnDied += OnDied;

        Movement = GetComponent<MovementBase>();
    }

    public void SetVisibility(bool visible)
    {
        IsVisible = visible;

        if (!isMyPlayer)
            Health.SetVisibility(visible);
    }

    public void SetMovementState(bool canMove)
    {
        if (Movement != null)
            Movement.SetMovement(canMove);
    }

    protected virtual void LateUpdate()
    {
        if (_provokator == null)
            return;

        var flipX = _provokator.transform.position.x < transform.position.x;
        Movement.SetFlipX(flipX);
    }

    public void Provoke(Unit source, float time)
    {
        _provokator = source;
        _provoke?.Kill();
        _provoke = Utilities.DoActionDelayed(ResetProvoke, time);
        IsProvoked = true;
        Movement.SetMovement(false);

        Debug.Log($"{name} provoked by {source}");
    }

    private void ResetProvoke()
    {
        _provokator = null;
        _provoke?.Kill();
        Movement.SetMovement(true);
        IsProvoked = false;
    }

    private void OnDamaged()
    {
        //transform.DOKill();
        //transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
    }

    protected virtual void OnDied()
    {
        gameObject.SetActive(false);

        if (needToKill)
            LevelManager.Instance.CheckIfComplete();
    }

    private void Start()
    {
        OnCreated?.Invoke(this);
    }

    private void OnDestroy()
    {
        Health.OnDamaged -= OnDamaged;
        Health.OnDied -= OnDied;

        //transform.DOKill();
        OnDestroyed?.Invoke(this);
    }
}
