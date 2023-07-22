using UnityEngine;

public class MovementBase : MonoBehaviour
{
    [field: SerializeField]
    public bool CanMove { get; protected set; } = true;
    [field: SerializeField]
    public bool IsMoving { get; protected set; }

    public float Speed => _speed;
    public float RunAwaySpeed => _runAwaySpeed;

    [SerializeField] protected float _speed = 0.7f;
    [SerializeField] protected float _runAwaySpeed = 1.4f;
    [SerializeField] protected float _runAwayRadius = 5f;

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

    public virtual void RunAway()
    {

    }

    public virtual void SetFlipX(bool value)
    {
        
    }
}
