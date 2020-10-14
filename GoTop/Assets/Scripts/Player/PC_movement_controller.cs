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
    
    void Start()
    {
        _playerMovement = GetComponent<Movement_controller>();
    }

    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonUp("Jump"))
        {
            _jump = true;
        }    
        
        _crouch = Input.GetKey(KeyCode.C);

        if (Input.GetKey(KeyCode.E))
            _playerMovement.StartCasting();
        
    }

    void FixedUpdate()
    {
        _playerMovement.Move(_move, _jump, _crouch);
        _jump = false;
    }
}
