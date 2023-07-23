using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MovementBase
{
    public NavMeshAgent NavMesh => _agent;

    [Space]
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool useInput;
    [SerializeField] private bool debug;

    private Vector3 _target;
    private NavMeshAgent _agent;
    private Patrol _patrol;

    private Vector3 _normalScale = Vector3.one;
    private Vector3 _flippedScale = new Vector3(-1,1,1);

    protected override void Awake()
    {
        base.Awake();

        useInput = GetComponent<Player>() != null;

        _patrol = GetComponent<Patrol>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _target = transform.position;

    }

    protected override void Update()
    {
        IsMoving = CanMove && (_agent.velocity.x != 0 || _agent.velocity.y != 0);

        base.Update();

        if (!CanMove)
            return;

        SetTargetPosition();
        SetAgentPosition();

        if (_agent.velocity.x != 0 && CanMove)
        {
            transform.localScale = _agent.velocity.x < 0 ? _flippedScale : _normalScale;

            if (debug)
                Debug.Log($"flipx: {(_agent.velocity.x < 0)}");
        }
    }

    public override void SetFlipX(bool value)
    {
        transform.localScale = value ? _flippedScale : _normalScale;
    }

    private void SetTargetPosition()
    {
        if (useInput && Input.GetMouseButtonDown(0))
        {
            _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void SetAgentPosition()
    {
        _agent.SetDestination(new Vector3(_target.x, _target.y, 0));
    }

    public void SetTarget(Vector2 position)
    {
        _target = position;
    }

    public override void SetMovement(bool isEnabled)
    {
        base.SetMovement(isEnabled);

        if (gameObject.activeInHierarchy)
            _agent.isStopped = !CanMove;
    }

    public override void RunAway()
    {
        base.RunAway();

        if (_patrol != null)
        {
            _patrol.Timeout();

            if (gameObject.activeInHierarchy)
            {
                _agent.SetDestination((Random.insideUnitCircle + Vector2.one * 0.5f) * _runAwayRadius);
                _agent.speed = RunAwaySpeed;
            }

            var health = GetComponent<Health>();
            health.Restore();
        }
    }
}
