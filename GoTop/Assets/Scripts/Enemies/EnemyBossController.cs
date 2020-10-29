using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossController : EnemyArcherController
{
    [Header("Strike")]
    [SerializeField] private Transform _strikePoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _strikeRange;
    [SerializeField] private LayerMask _enemies;

    [Header("PowerStrike")]
    [SerializeField] private Collider2D _strikeCollider;
    [SerializeField] private int _powerStrikeDamage;
    [SerializeField] private float _powerStrikeSpeed;
    [SerializeField] private float _powerStrikeRange;

    [SerializeField] private float _waitTime;

    private float _currentStrikeRange;
    private bool _fightStarted;
    private EnemyState _stateOnHold;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_currentState == EnemyState.Move && _attacking)
        {
            TurnToPlayer();
            if (CanAttack())
                ChangeState(_stateOnHold);
        }
    }

    private EnemyState[] _attackStates = { EnemyState.Strike, EnemyState.PowerStrike, EnemyState.Shoot };
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_strikePoint.position, new Vector3(_strikeRange, _strikeRange, 0));
    }

    protected void Strike()
    {
        Collider2D player = Physics2D.OverlapBox(_strikePoint.position, new Vector2(_strikeRange, _strikeRange), 0, _enemies);
        if (player != null)
        {
            Player_controller playerController = player.GetComponent<Player_controller>();
            if (playerController != null)
                playerController.TakeDamage(_damage);
        }
    }

    protected void StrikeWithPower()
    {
        _strikeCollider.enabled = true;
        _enemyRB.velocity = transform.right * _powerStrikeSpeed;
    }

    protected void EndPowerStrike()
    {
        _strikeCollider.enabled = false;
        _enemyRB.velocity = Vector2.zero;
    }

    protected override void CheckPlayerInRange()
    {
        if (_player == null)
            return;

        if (Vector2.Distance(transform.position, _player.transform.position) < _angerRange)
        {
            _isAngry = true;
            if (!_fightStarted)
            {
                StopCoroutine(ScanForPlayer());
                StartCoroutine(BeginNewCircle());
            }
        }
        else
            _isAngry = false;
    }

    protected override void ChangeState(EnemyState state)
    {
        if (_currentState == state)
            return;

        if (state == EnemyState.PowerStrike || state == EnemyState.Strike)
        {
            _attacking = true;
            _currentStrikeRange = state == EnemyState.Strike ? _strikeRange : _powerStrikeRange;
            _enemyRB.velocity = Vector2.zero;

            if (!CanAttack())
            {
                _stateOnHold = state;
                state = EnemyState.Move;
                
            }
        }
        base.ChangeState(state);
    }

    private bool CanAttack()
    {
        return Vector2.Distance(transform.position, _player.transform.position) < _currentStrikeRange;
    }

    protected override void DoStateAction()
    {
        base.DoStateAction();

        switch (_currentState)
        {
            case EnemyState.Strike:
                Strike();
                break;
            case EnemyState.PowerStrike:
                StrikeWithPower();
                break;
        }
    }

    protected override void EndState()
    {
        base.EndState();
        if (_currentState == EnemyState.PowerStrike)
        {
            EndPowerStrike();
        }

        _attacking = false;
        StartCoroutine(BeginNewCircle());
    }

    private IEnumerator BeginNewCircle()
    {
        if (_fightStarted)
        {
            ChangeState(EnemyState.Idle);
            CheckPlayerInRange();
            if (!_isAngry)
            {
                _fightStarted = false;
                StartCoroutine(ScanForPlayer());
                yield break;
            }
            yield return new WaitForSeconds(_waitTime);
        }
        _fightStarted = true;
        TurnToPlayer();
        ChooseNextAttackState();
    }

    protected void ChooseNextAttackState()
    {
        int state = Random.Range(0, _attackStates.Length);
        ChangeState(_attackStates[state]);
    }

    protected override void TryToDamage(Collider2D enemy)
    {
        if (_currentState == EnemyState.Idle || _currentState == EnemyState.Shoot)
            return;

        Player_controller player = enemy.GetComponent<Player_controller>();

        if (player == null)
            return;

        player.TakeDamage(_powerStrikeDamage, DamageTypes.PowerStrike, transform);
    }
}
