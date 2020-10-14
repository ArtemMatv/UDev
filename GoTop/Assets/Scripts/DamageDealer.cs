using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]private int _damage;
    [SerializeField] private float _timeDalay;
    private Player_controller _player;
    private DateTime _lastEncounter;

    private void OnTriggerEnter2D(Collider2D info)
    {
        if ((DateTime.Now - _lastEncounter).TotalSeconds < 0.1f)
            return;

        _lastEncounter = DateTime.Now;
        _player = info.GetComponent<Player_controller>();
        if (_player != null){
            _player.ChangeHp(-_damage);
            
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (_player == collider.GetComponent<Player_controller>())
            _player = null;
    }

    void Update()
    {
        if (_player != null && (DateTime.Now - _lastEncounter).TotalSeconds > _timeDalay)
        {
            _player.ChangeHp(-_damage);
            _lastEncounter = DateTime.Now;
        }
    }
}
