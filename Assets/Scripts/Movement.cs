using UnityEngine;

public class Movement : MovementBase
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private SpriteRenderer _renderer;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    protected override void Update()
    {
        ProcessInputs();

        base.Update();
    }

    private void FixedUpdate()
    {
        if (_rigidbody == null)
            return;

        Move();
    }

    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDirection = CanMove ? new Vector2(moveX, moveY).normalized : Vector2.zero;

        if (CanMove && _renderer != null && moveX != 0)
            _renderer.flipX = moveX < 0;

        IsMoving = CanMove && (_moveDirection.x != 0 || _moveDirection.y != 0);
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * moveSpeed;
    }
}
