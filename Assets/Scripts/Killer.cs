using TweaxxGames.JamRaid;
using UnityEngine;

public enum KillerBehavior
{ 
    Stealth,
}

public class Killer : Unit
{
    [field: SerializeField]
    public KillerBehavior Behavior { get; private set; }

    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private float chaseRadius = 5;
    [SerializeField] private float attackInterval = 0.1f;

    private MeleeKillAbility _killAbility;
    private AgentMovement _movement;
    private Unit _target;
    private Vector2 _initialPosition;
    private float _currentInterval;

    private void Start()
    {
        _initialPosition = transform.position;
        _movement = GetComponent<AgentMovement>();
        _killAbility = GetComponent<MeleeKillAbility>();
        SetBehavior(Behavior);
    }

    public void SetBehavior(KillerBehavior behavior)
    {
        Behavior = behavior;
        _movement.SetMovement(false);
    }

    protected override void OnDied()
    {
        base.OnDied();


    }

    private void Update()
    {
        switch (Behavior)
        {
            case KillerBehavior.Stealth:
                StealthBehavior();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player) && player.IsVisible)
        {
            SoundManager.Instance.PlaySound(SoundType.Attention);

            _target = player;
        }
    }

    private void StealthBehavior()
    {
        if (_currentInterval > 0)
            _currentInterval -= Time.deltaTime;

        if (_target != null && Vector2.Distance(transform.position, _target.transform.position) < chaseRadius)
        {
            _movement.SetMovement(true);
            _movement.SetTarget(_target.transform.position);

            if (_currentInterval <= 0 && Vector2.Distance(transform.position, _target.transform.position) < attackRadius)
            {
                _currentInterval = attackInterval;
                _killAbility.Use();
            }
        }
        else
        {
            _target = null;
            if (Vector2.Distance(transform.position, _initialPosition) > _movement.NavMesh.stoppingDistance)
            {
                _movement.SetTarget(_initialPosition);
            }
            else
            {
                _movement.SetMovement(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}
