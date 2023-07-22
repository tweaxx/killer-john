using UnityEngine;

public class MovementBase : MonoBehaviour
{
    [field: SerializeField]
    public bool CanMove { get; protected set; } = true;
    [field: SerializeField]
    public bool IsMoving { get; protected set; }

    private Animator _animator;

    private const string RUN = "Run";

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        _animator.SetBool(RUN, IsMoving);
    }

    public virtual void SetMovement(bool isEnabled)
    {
        CanMove = isEnabled;
    }
}
