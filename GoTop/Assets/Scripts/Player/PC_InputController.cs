﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Movement_controller))]
public class PC_InputController : MonoBehaviour
{
    Movement_controller _playerMovement;
    DateTime _strikeClickTime;
    float _move;
    float _verticalMove;
    bool _jump;
    bool _crouch;
    bool _canAtack;

    private void Start()
    {
        _playerMovement = GetComponent<Movement_controller>();
    }
    // Start is called before the first frame update

    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        _verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonUp("Jump"))
        {
            _jump = true;
        }

        _crouch = Input.GetKey(KeyCode.C);

        if (Input.GetButtonUp("Fire2"))
            _playerMovement.StartCasting();

        if (Input.GetButtonDown("Fire1"))
        {
            _strikeClickTime = DateTime.Now;
            _canAtack = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            float holdTime = (float)(DateTime.Now - _strikeClickTime).TotalSeconds;
            if (_canAtack)
                _playerMovement.StartStrike(holdTime);
            _canAtack = false;
        }

        if ((DateTime.Now - _strikeClickTime).TotalSeconds >= _playerMovement.ChargeTime * 2 && _canAtack)
        {
            _playerMovement.StartStrike(_playerMovement.ChargeTime);
            _canAtack = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
            _playerMovement.UseLadder();
        
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_move, _jump, _crouch, _verticalMove);
        _jump = false;
    }
}