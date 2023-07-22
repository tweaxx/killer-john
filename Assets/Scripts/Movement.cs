using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private SpriteRenderer _renderer;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;

    private Animator _animator;

    private const string RUN = "Run";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ProcessInputs();
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

        _moveDirection = new Vector2(moveX, moveY).normalized;

        if (_renderer != null && moveX != 0)
            _renderer.flipX = moveX < 0;

        _animator.SetBool(RUN, moveX != 0 || moveY != 0);
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * moveSpeed;
    }
}
