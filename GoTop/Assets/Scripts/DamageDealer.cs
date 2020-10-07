using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]private int _damage;
    private Player_controller _player;

    private void OnTriggerEnter2D(Collider2D info)
    {
        _player = info.GetComponent<Player_controller>();
        if (_player != null){
            _player.ChangeHp(-_damage);
            
        }
    }
}
