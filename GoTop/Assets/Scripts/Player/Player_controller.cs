using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxMP;
    private int _currentHP;
    private int _currentMP;

    Movement_controller _playerMovement;

    private bool _canBeDamaged = true;
    void Start()
    {
        _currentHP = _maxHP;
        _currentMP = _maxMP;
        _playerMovement = GetComponent<Movement_controller>();
        _playerMovement.OnGetHurt += OnGetHurt;
    }

    public void TakeDamage(int damage, DamageTypes type = DamageTypes.Casual, Transform enemy = null)
    {
        if (!_canBeDamaged)
            return;

        _currentHP -= damage;
        Debug.Log(damage);

        switch (type)
        {
            case DamageTypes.PowerStrike:
                _playerMovement.GetHurt(enemy.position);
                break;

        }
        if (_currentHP <= 0)
        {
            _playerMovement.OnDeathStart();
        }
    }

    private void OnGetHurt(bool canBeDamaged)
    {
        _canBeDamaged = canBeDamaged;
    }

    public void RestoreHP(int hp)
    {
        _currentHP += hp;
        if (_currentHP > _maxHP)
            _currentHP = _maxHP;
    }

    public bool ChangeMP(int value)
    {
        if (value < 0 && _currentMP < Mathf.Abs(value))
            return false;

        _currentMP += value;
        if (_currentMP > _maxMP)
            _currentMP = _maxMP;

        return true;
    }
}
