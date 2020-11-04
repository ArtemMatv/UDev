using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player_controller : MonoBehaviour
{
    private ServiceManager _serviceManager;

    [SerializeField] private int _maxHP;
    [SerializeField] private int _maxMP;
    [SerializeField] private int _coins;
    private int _currentHP;
    private int _currentMP;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _mpBar;
    [SerializeField] private TextMeshProUGUI _coinsBar;

    Movement_controller _playerMovement;
    Vector2 _startPosition;

    private bool _canBeDamaged = true;
    void Start()
    {
        _startPosition = transform.position;
        _playerMovement = GetComponent<Movement_controller>();
        _playerMovement.OnGetHurt += OnGetHurt;

        _currentHP = _maxHP;
        _currentMP = _maxMP;
        _hpBar.maxValue = _maxHP;
        _hpBar.value = _maxHP;
        _mpBar.maxValue = _maxMP;
        _mpBar.value = _maxMP;

        _coinsBar.text += _coins;

        _serviceManager = ServiceManager.Instance;
    }

    public void TakeDamage(int damage, DamageTypes type = DamageTypes.Casual, Transform enemy = null)
    {
        if (!_canBeDamaged)
            return;

        _currentHP -= damage;

        _hpBar.value = _currentHP;

        switch (type)
        {
            case DamageTypes.PowerStrike:
                _playerMovement.GetHurt(enemy.position);
                break;
        }
        if (_currentHP <= 0)
        {
            _playerMovement.OnDeathOrRespawn(true);
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

        _hpBar.value = _currentHP;
    }

    public bool ChangeMP(int value)
    {
        if (value < 0 && _currentMP < Mathf.Abs(value))
            return false;

        _currentMP += value;
        if (_currentMP > _maxMP)
            _currentMP = _maxMP;

        _mpBar.value = _currentMP;
        return true;
    }

    public void OnDeath()
    {
        _serviceManager.Restart();

        /*
         * For returning to start
         * _playerMovement.OnDeathOrRespawn(false);
         * _currentHP = _maxHP;
         * _currentMP = _maxMP;
         * _hpBar.value = _currentHP;
         * _mpBar.value = _currentMP;
         * transform.position = _startPosition;
         */
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        _coinsBar.text = "Coins: " + _coins;
    }
}
