using System;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField]private int _damage;
    [SerializeField] private float _timeDelay;
    private Player_controller _player;
    private DateTime _lastInCounter;

    private void OnTriggerEnter2D(Collider2D info)
    {
        if ((DateTime.Now - _lastInCounter).TotalSeconds < 0.1f)
            return;

        _lastInCounter = DateTime.Now;

        _player = info.GetComponent<Player_controller>();
        if (_player != null){
            _player.ChangeHp(-_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D info)
    {
        if (_player == info.GetComponent<Player_controller>())
            _player = null;
    }

    private void Update()
    {
        if (_player != null && (DateTime.Now - _lastInCounter).TotalSeconds > _timeDelay)
        {
            _player.ChangeHp(-_damage);
            _lastInCounter = DateTime.Now;
        }
    }
}
