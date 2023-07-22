using UnityEngine;

public class Policeman : Unit
{
    [SerializeField] private Visor visor;

    protected override void Awake()
    {
        base.Awake();
        visor.OnEnter += OnEnteredVisor;
    }

    private void OnDestroy()
    {
        visor.OnEnter -= OnEnteredVisor;
    }

    private void OnEnteredVisor(GameObject obj)
    {
        if (obj.TryGetComponent(out Player player))
        {
            //Debug.Log($"{name} spotted Player!");
        }
    }

    private void Update()
    {
        if (visor == null) return;


    }
}
