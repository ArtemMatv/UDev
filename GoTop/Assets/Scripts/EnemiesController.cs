using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private int _hp;

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
            OnDeath();
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
