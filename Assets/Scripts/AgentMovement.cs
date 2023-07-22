using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public NavMeshAgent NavMesh => _agent;
    public bool CanMove { get; private set; } = true;

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private bool useInput;

    private Vector3 _target;
    private NavMeshAgent _agent;

    private Vector3 _normalScale = Vector3.one;
    private Vector3 _flippedScale = new Vector3(-1,1,1);

    private void Awake()
    {
        useInput = GetComponent<Player>() != null;

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _target = transform.position;
    }

    private void Update()
    {
        if (!CanMove)
            return;

        SetTargetPosition();
        SetAgentPosition();

        if (_agent.velocity.x != 0)
            transform.localScale = _agent.velocity.x < 0 ? _flippedScale : _normalScale;
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
        _agent.SetDestination(new Vector3(_target.x, _target.y, transform.position.z));
    }

    public void SetTarget(Vector2 position)
    {
        _target = position;
    }

    public void Stop()
    {
        if (gameObject.activeInHierarchy)
            _agent.isStopped = true;

        CanMove = false;
    }

    public void Resume()
    {
        if (gameObject.activeInHierarchy)
            _agent.isStopped = false;

        CanMove = true;
    }
}
