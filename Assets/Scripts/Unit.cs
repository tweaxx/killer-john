using System;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    public static event Action<Unit> OnCreated;
    public static event Action<Unit> OnDestroyed;

    public Health Health { get; private set; }
    public MovementBase Movement { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        Health.OnDamaged += OnDamaged;
        Health.OnDied += OnDied;

        Movement = GetComponent<MovementBase>();
    }

    public void SetMovementState(bool canMove)
    {
        if (Movement != null)
            Movement.SetMovement(canMove);
    }

    private void OnDamaged()
    {
        //transform.DOKill();
        //transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
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
