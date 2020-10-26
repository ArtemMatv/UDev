using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class EnemyControllerBase : MonoBehaviour
{
    protected Rigidbody2D _enemyRB;
    protected Animator _enemyAnimator;
    protected Vector2 _startPoint;
    protected EnemyState _currentState;

    protected float _lastStateChange;
    protected float _timeToNextChange;

    [SerializeField] private float _maxStateTime;
    [SerializeField] private float _minStateTime;
    [SerializeField] private EnemyState[] _availableStates;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _checkRange;
    [SerializeField] private Transform _wallCheck;

    protected bool _faceRight = true;

    protected virtual void Start()
    {
        _startPoint = transform.position;
        _enemyRB = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
        if (IsGroundEnding() || IsInRange() || Physics2D.OverlapPoint(_wallCheck.position, _whatIsGround))
            Flip();

        if (_currentState == EnemyState.Move)
            Move();
    }

    private bool IsInRange()
    {
        return _checkRange && ((transform.position.x - _startPoint.x < -_range) ||
            (transform.position.x - _startPoint.x > _range));
    }

    protected virtual void Update()
    {
        if (Time.time - _lastStateChange > _timeToNextChange)
        {
            GetRandomState();
        }
    }

    protected virtual void Move()
    {
        _enemyRB.velocity = transform.right * new Vector2(_speed, _enemyRB.velocity.y);
    }

    protected virtual void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_range * 2, 0.5f, 0));
    }

    private bool IsGroundEnding()
    {
        return !Physics2D.OverlapPoint(_groundCheck.position, _whatIsGround);
    }

    protected void GetRandomState()
    {
        int state = Random.Range(0, _availableStates.Length);

        if (_currentState == EnemyState.Idle && _availableStates[state] == EnemyState.Idle)
        {
            GetRandomState();
        }
        _timeToNextChange = Random.Range(_minStateTime, _maxStateTime);
        ChangeState(_availableStates[state]);
    }

    protected virtual void ChangeState(EnemyState state)
    {
        if (_currentState != EnemyState.Idle)
            _enemyAnimator.SetBool(_currentState.ToString(), false);

        if (state != EnemyState.Idle)
            _enemyAnimator.SetBool(state.ToString(), true);

        _currentState = state;
        _lastStateChange = Time.time;
    }
}

public enum EnemyState
{
    Idle,
    Move,
    Shoot,
    Strike,
    PowerStrike
}

