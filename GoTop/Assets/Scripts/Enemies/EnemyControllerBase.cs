﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class EnemyControllerBase : MonoBehaviour
{
    protected Rigidbody2D _enemyRB;
    protected Animator _enemyAnimator;
    protected Vector2 _startPoint;
    protected EnemyState _currentState;

    [Header("HP")]
    [SerializeField] protected Slider _hpBar;
    [SerializeField] protected int _maxHP;
    protected int _currentHp;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _checkRange;
    [SerializeField] private Transform _wallCheck;
    protected bool _faceRight = true;

    [Header("State changes")]
    [SerializeField] private float _maxStateTime;
    [SerializeField] private float _minStateTime;
    [SerializeField] private EnemyState[] _availableStates;
    protected float _lastStateChange;
    protected float _timeToNextChange;

    [Header("Collision damage")]
    [SerializeField] private DamageTypes _collisionDamageType;
    [SerializeField] protected int _collisionDamage;
    [SerializeField] protected float _collisionTimeDelay;
    protected float _lastDamageTime;

    [Header("Fight Settings")]
    [SerializeField] protected int _hurtDelay;
    private float _lastHurtTime;

    Quaternion _hpBarRotation;
    void Awake()
    {
        _hpBarRotation = _hpBar.transform.rotation;
    }
    void LateUpdate()
    {
        _hpBar.transform.rotation = _hpBarRotation;
    }

    protected virtual void Start()
    {
        _startPoint = transform.position;
        _enemyRB = GetComponent<Rigidbody2D>();
        _enemyAnimator = GetComponent<Animator>();
        _currentHp = _maxHP;
        _hpBar.maxValue = _maxHP;
        _hpBar.value = _maxHP;
        
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
        int state = Random.Range(0, _availableStates.Length - 1);

        if (_currentState == EnemyState.Idle && _availableStates[state] == EnemyState.Idle)
        {
            GetRandomState();
        }
        _timeToNextChange = Random.Range(_minStateTime, _maxStateTime);
        ChangeState(_availableStates[state]);
    }

    protected virtual void ChangeState(EnemyState state)
    {
        
        if (_currentState == EnemyState.Death)
            return;

        if (_currentState != EnemyState.Idle)
            _enemyAnimator.SetBool(_currentState.ToString(), false);

        if (state == EnemyState.Hurt)
            GetHurt();
        else if (state != EnemyState.Idle)
            _enemyAnimator.SetBool(state.ToString(), true);

        _currentState = state;
        _lastStateChange = Time.time;

        switch (_currentState)
        {
            case EnemyState.Idle:
                _enemyRB.velocity = Vector2.zero;
                break;
            case EnemyState.Move:
                _startPoint = transform.position;
                break;
        }
    }

    protected virtual void EndState()
    {
        if (_currentState == EnemyState.Death)
            EndDeath();

        if (_currentState == EnemyState.Hurt)
            EndHurt();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        TryToDamage(collision.collider);
    }

    protected virtual void TryToDamage(Collider2D enemy)
    {
        if (Time.time - _lastDamageTime < _collisionTimeDelay)
            return;

        Player_controller player = enemy.GetComponent<Player_controller>();

        if (player != null)
            player.TakeDamage(_collisionDamage, _collisionDamageType, transform);
        
    }

    public virtual void TakeDamage(int damage, DamageTypes type = DamageTypes.Casual, Transform player = null)
    {
        if (Time.time - _lastHurtTime < _hurtDelay)
            return;

        _currentHp -= damage;
        _lastHurtTime = Time.time;
        if (_currentHp > 0)
            ChangeState(EnemyState.Hurt);
        else
            ChangeState(EnemyState.Death);

        _hpBar.value = _currentHp;
    }

    public virtual void GetHurt()
    {
        _enemyRB.velocity = Vector2.zero;
        _enemyAnimator.SetBool("Hurt", true);
    }

    private void EndHurt()
    {
        _enemyAnimator.SetBool("Hurt", false);
    }

    public void OnDeathStart()
    {
        _enemyRB.velocity = Vector2.zero;
        _enemyRB.bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
    }

    public virtual void EndDeath()
    {
        Destroy(gameObject);
    }

    protected virtual void DoStateAction() { }
}

public enum EnemyState
{
    Idle,
    Move,
    Shoot,
    Strike,
    PowerStrike,
    Hurt,
    Death
}

