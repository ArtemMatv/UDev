using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement_controller : MonoBehaviour
{
    private Rigidbody2D _playerRD;
    private Animator _playerAnimator;

    [Header("Horizontal movement")]
    [SerializeField]private  float _speed;
    private bool _faceRight = true;

    [Header("Jumping")] 
    [SerializeField]private Transform _groundCheck;
    [SerializeField]private float _jumpForce;
    [SerializeField]private bool _airControll;
    [SerializeField] private float _groundedRadius;
    private bool _grounded;
    private bool _doubleJumpAvailability = true;

    [Header("Crouching")]
    [SerializeField]private Collider2D _headCollider;
    [SerializeField]private Transform _cellCheck;
    [SerializeField]private float _crouchSpeedReduce;
    [SerializeField] private float _crouchingRadius;
    private bool _canStand;

    [Header("Raising ladders")]
    [SerializeField] private LayerMask _whatIsLadder;
    [SerializeField] private Transform _raisingCheck;
    [SerializeField] private Transform _goDownCheck;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private float _verticalRadius;
    [SerializeField] private Collider2D[] _playerColliders;
    private bool _canRaise;
    private bool _canGoDown;
    private bool _movingOnLadder;

    [Header("Ground settings")]
    [SerializeField]private LayerMask _whatIsGround;
    
    void Start()
    {
        _playerRD = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(_groundCheck.position, _groundedRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _crouchingRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_raisingCheck.position, _verticalRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_goDownCheck.position, _verticalRadius);
    }

    void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float move, bool jump, bool crouch, float raising)
    {
        #region Movement
        float speedModificator =  crouch ? _crouchSpeedReduce : 1;

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
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundedRadius, _whatIsGround);
        if(jump && (_doubleJumpAvailability || _grounded))
        {
            _playerRD.AddForce(Vector2.up * _jumpForce);
            _doubleJumpAvailability = _grounded;
        }
        #endregion

        #region Crouch
        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _crouchingRadius, _whatIsGround);

        if (_canStand)
            _headCollider.enabled = !crouch;
        #endregion

        #region RaisingLadder
        

        _canRaise = Physics2D.OverlapCircle(_raisingCheck.position, _verticalRadius, _whatIsLadder);
        _canGoDown = Physics2D.OverlapCircle(_goDownCheck.position, _verticalRadius, _whatIsLadder);
        _movingOnLadder = ((_canRaise && raising > 0) || (_canGoDown && raising < 0)) && !_grounded;

        if (raising == 0 && (_canGoDown || _canRaise) && !_grounded)
        {
            _playerRD.gravityScale = 0;
            _playerRD.mass = 0;
            _playerRD.velocity = new Vector2(0, raising * _verticalSpeed);
        }
        else if ((_canRaise && raising > 0) || (_canGoDown && raising < 0))
        {
            _playerRD.velocity = new Vector2(0, raising * _verticalSpeed);
        
            if (!_grounded || _canGoDown)
               foreach(Collider2D item in _playerColliders)
               {
                   item.enabled = false;
               }
        }
        else
        {
            if (_grounded)
                foreach (Collider2D item in _playerColliders)
                {
                   item.enabled = true;
                }
            _playerRD.gravityScale = 3;
            _playerRD.mass = 1;
        }

        #endregion

        #region Animation
        _playerAnimator.SetFloat("Speed", Mathf.Abs(move));
        _playerAnimator.SetBool("Jump", !_grounded);
        _playerAnimator.SetBool("Crouch", !_headCollider.enabled);
        #endregion
    }
}
