using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [SerializeField] private int _maxHp;
    private int _currentHp;
    void Start()
    {
        _currentHp = _maxHp;
    }

    public void ChangeHp(int value)
    {
        _currentHp += value;

        if (_currentHp > _maxHp)
        {
            _currentHp = _maxHp;
        }
        else if (_currentHp <= 0)
        {
            OnDeath();
        }
        Debug.Log("value = " + value);
        Debug.Log("Current HP = " + _currentHp);
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
