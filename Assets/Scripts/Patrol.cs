using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float waitMin = 1;
    [SerializeField] private float waitMax = 5;

    [Space]
    [SerializeField] private float _currentTime;
    [SerializeField] private float _remainingDistance;

    private AgentMovement _agentMovement;

    private void Awake()
    {
        if (!TryGetComponent(out _agentMovement))
            enabled = false;

        SetTime();
    }

    private void SetTime()
    {
        _currentTime = Random.Range(waitMin, waitMax);
    }

    public void Timeout()
    {
        _currentTime = waitMax;
    }

    public Vector2 GetRandomPoint()
    {
        return (Vector2)transform.position + Random.insideUnitCircle * radius;
    }

    private void Update()
    {
        if (!_agentMovement.CanMove)
            return;

        _remainingDistance = _agentMovement.NavMesh.remainingDistance;

        if (_remainingDistance <= _agentMovement.NavMesh.stoppingDistance)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                SetTime();
                _agentMovement.SetTarget(GetRandomPoint());
                _agentMovement.NavMesh.speed = _agentMovement.Speed;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
