using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public List<Unit> Units = new List<Unit>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Unit.OnCreated += Add;
            Unit.OnDestroyed += Remove;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            Unit.OnCreated -= Add;
            Unit.OnDestroyed -= Remove;
        }
    }

    private void Add(Unit unit)
    {
        Units.Add(unit);
    }

    private void Remove(Unit unit)
    {
        Units.Remove(unit);
    }
}
