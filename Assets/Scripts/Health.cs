using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDied;
    public event Action OnDamaged;

    public bool IsAlive => health > 0;
    public bool IsDead => health <= 0;

    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        //Debug.Log($"{name} takes {damage} damage!");

        health = Mathf.Max(health - damage, 0);
        OnDamaged?.Invoke();

        if (IsDead)
        {
            //Debug.Log($"{name} died!");
            OnDied?.Invoke();
        }
    }
}
