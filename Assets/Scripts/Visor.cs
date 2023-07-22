using System;
using UnityEngine;

public class Visor : MonoBehaviour
{
    public event Action<GameObject> OnEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(collision.gameObject);
    }
}
