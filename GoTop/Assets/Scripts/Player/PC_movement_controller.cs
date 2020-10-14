using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement_controller))]
public class PC_movement_controller : MonoBehaviour
{
    Movement_controller _playerMovement;

    float _move;
    bool _jump;
    bool _crouch;
    float _raising;
    
    void Start()
    {
        _playerMovement = GetComponent<Movement_controller>();
    }

    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
      
        _jump = Input.GetButtonUp("Jump");
        
        _crouch = Input.GetKey(KeyCode.C);

        _raising = Input.GetAxisRaw("Vertical");
        
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_move, _jump, _crouch, _raising);
        _jump = false;
    }
}
