using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement_controller : MonoBehaviour
{
    private Rigidbody2D _playerRD;
    private Animator _playerAnimator;
    private Player_controller _playerController;

    private bool _faceRight = true;
    private bool _canMove = true;
    private float _gravity;

    [Header("Horizontal movement")]
    [SerializeField] private float _speed;

    [Header("Jumping")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _airControll;
    private bool _grounded;
    private bool _doubleJumpAvailability = true;

    [Header("Crouching")]
    [SerializeField] private Collider2D _headCollider;
    [SerializeField] private Transform _cellCheck;
    [Range(0, 1)]
    [SerializeField] private float _crouchSpeedReduce;
    private bool _canStand;

    [Header("Ground settings")]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _radius;

    [Header("Casting")]
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireBallSpeed;
    [SerializeField] private int _castCost;
    private bool _isCasting;

    [Header("Strike")]
    [SerializeField] private Transform _strikePoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemies;
    private bool _isStriking;

    [Header("Power Strike")]
    [SerializeField] private float _chargeTime;
    public float ChargeTime => _chargeTime;
    [SerializeField] private float _powerStrikeSpeed;
    [SerializeField] private int _powerStrikeDamage;
    [SerializeField] private Collider2D _strikeCollider;
    [SerializeField] private int _powerStrikeCost;
    private List<EnemiesController> _damageEnemies = new List<EnemiesController>();


    [Header("Lader")]
    [SerializeField] private LayerMask _whatIsLadder;
    [SerializeField] private float _ladderSpeed;
    [SerializeField] private Collider2D[] _collidersToDisable;
    [SerializeField] private Transform _raiseCheck;
    [SerializeField] private float _raiseCheckHeight;
    private Vector3 _raiseCheckVector => new Vector3(0.01f, _raiseCheckHeight);
    [SerializeField] private Transform _goDownCheck;
    private bool _raising = false;

    void Start()
    {
        _playerRD = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerController = GetComponent<Player_controller>();
        _gravity = _playerRD.gravityScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
        Gizmos.color = UnityEngine.Color.black;
        Gizmos.DrawWireSphere(_strikePoint.position, _attackRange);
        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(_raiseCheck.position, _raiseCheckVector);
        Gizmos.color = UnityEngine.Color.yellow;
        Gizmos.DrawWireSphere(_goDownCheck.position, 0.01f);
    }

    void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float move, bool jump, bool crouch, float vervicalMove)
    {
        if (_raising)
        {
            Debug.Log("Raising...");
            if (Physics2D.OverlapBox(_raiseCheck.position, _raiseCheckVector, 0, _whatIsLadder) && vervicalMove > 0
                || Physics2D.OverlapPoint(_goDownCheck.position, _whatIsLadder) && vervicalMove < 0)
            {
                _playerRD.velocity = new Vector2(0, vervicalMove * _ladderSpeed);
            }
            else
            {
                _playerRD.velocity = Vector2.zero;
            }

            if (!(Physics2D.OverlapBox(_raiseCheck.position, _raiseCheckVector, 0, _whatIsLadder)
                || Physics2D.OverlapPoint(_goDownCheck.position, _whatIsLadder)))
            {
                Debug.Log("Out of ladder");
                _canMove = true;
                _raising = false;
                _playerRD.gravityScale = _gravity;

                foreach (Collider2D collider in _collidersToDisable)
                {
                    collider.enabled = true;
                }
            }
        }

        if (!_canMove)
            return;

        #region Movement
        float speedModificator = !_headCollider.enabled ? _crouchSpeedReduce : 1;

        if ((move != 0 && (_grounded || _airControll)))
        {
            _playerRD.velocity = new Vector2(_speed * move * speedModificator, _playerRD.velocity.y);
        }

        if ((move > 0 && !_faceRight) || (move < 0 && _faceRight))
        {
            Flip();
        }
        #endregion

        #region Jump
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        if (jump && (_doubleJumpAvailability || _grounded))
        {
            _playerRD.AddForce(Vector2.up * _jumpForce);
            _doubleJumpAvailability = _grounded;
        }
        #endregion

        #region Crouch
        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);

        if (_canStand)
            _headCollider.enabled = !crouch;
        #endregion

        #region Animation
        _playerAnimator.SetFloat("Speed", Mathf.Abs(move));
        _playerAnimator.SetBool("Jump", !_grounded);
        _playerAnimator.SetBool("Crouch", !_headCollider.enabled);
        #endregion
    }



    public void StartCasting()
    {
        if (_isCasting || !_playerController.ChangeMP(-_castCost))
            return;

        _isCasting = true;
        _playerAnimator.SetBool("Casting", true);
    }

    private void CastFire()
    {
        GameObject fireball = Instantiate(_fireBall, _firePoint.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = transform.right * _fireBallSpeed;
        fireball.GetComponent<SpriteRenderer>().flipX = !_faceRight;
        Destroy(fireball, 5f);
    }

    private void EndCasting()
    {
        _isCasting = false;
        _playerAnimator.SetBool("Casting", false);
    }


    public void StartStrike(float holdTime)
    {
        if (_isStriking)
            return;

        if (holdTime >= _chargeTime)
        {
            if (!_playerController.ChangeMP(-_powerStrikeCost))
                return;

            _playerAnimator.SetBool("PowerStrike", true);
            _canMove = false;
        }
        else
            _playerAnimator.SetBool("Strike", true);

        _isStriking = true;
    }

    private void Strike()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_strikePoint.position, _attackRange, _enemies);

        if (enemies != null)
            foreach (var enemy in enemies)
                enemy.GetComponent<EnemiesController>().TakeDamage(_damage);
    }

    private void EndStrike()
    {
        _isStriking = false;
        _playerAnimator.SetBool("Strike", false);
    }

    private void StartPowerStrike()
    {
        _playerRD.velocity = transform.right * _powerStrikeSpeed;
        _strikeCollider.enabled = true;
    }

    private void DisablePowerStrike()
    {
        _playerRD.velocity = Vector2.zero;
        _strikeCollider.enabled = false;
        _damageEnemies.Clear();
    }

    private void EndPowerStrike()
    {
        _playerAnimator.SetBool("PowerStrike", false);
        _isStriking = false;
        _canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_strikeCollider.enabled)
            return;


        EnemiesController enemy = collision.collider.GetComponent<EnemiesController>();

        if (enemy == null || _damageEnemies.Contains(enemy))
            return;

        enemy.TakeDamage(_powerStrikeDamage);
        _damageEnemies.Add(enemy);
    }

    public void UseLadder()
    {
        bool goTop = Physics2D.OverlapBox(_raiseCheck.position, _raiseCheckVector, 0, _whatIsLadder);
        bool goDown = Physics2D.OverlapPoint(_goDownCheck.position, _whatIsLadder);

        if (goTop || goDown)
        {
            _canMove = !_canMove;
            _raising = !_raising;
            _playerRD.gravityScale = _playerRD.gravityScale == 0 ? _gravity : 0;

            foreach (Collider2D collider in _collidersToDisable)
            {
                collider.enabled = !collider.enabled;
            }
        }
    }
}
