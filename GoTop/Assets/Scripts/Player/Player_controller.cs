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
        if (_currentHP <= 0)
            OnDeath();

        switch (type)
        {
            case DamageTypes.PowerStrike:
                _playerMovement.GetHurt(enemy.position);
                break;

        }

        Debug.Log("value = " + damage);
        Debug.Log("Current HP = " + _currentHP);
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

        Debug.Log("value = " + hp);
        Debug.Log("Current HP = " + _currentHP);
    }

    public bool ChangeMP(int value)
    {
        Debug.Log("MP value = " + value);

        if (value < 0 && _currentMP < Mathf.Abs(value))
            return false;

        _currentMP += value;
        if (_currentMP > _maxMP)
            _currentMP = _maxMP;

        Debug.Log("Current MP" + _currentMP);
        return true;
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
