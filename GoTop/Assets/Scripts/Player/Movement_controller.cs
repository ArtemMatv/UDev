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
    private bool _grounded;
    private bool _doubleJumpAvailability = true;

    [Header("Crouching")]
    [SerializeField]private Collider2D _headCollider;
    [SerializeField]private Transform _cellCheck;
    [SerializeField]private float _crouchSpeedReduce;
    private bool _canStand;

    [Header("Ground settings")]
    [SerializeField]private LayerMask _whatIsGround;
    [SerializeField]private float _radius;
    
    void Start()
    {
        _playerRD = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
    }

    void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float move, bool jump, bool crouch)
    {
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
        if(jump && (_doubleJumpAvailability || _grounded))
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
}
