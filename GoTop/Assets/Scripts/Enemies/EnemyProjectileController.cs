using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage;
    private DateTime _lastInCounter;

    void OnTriggerEnter2D(Collider2D info)
    {
        if ((DateTime.Now - _lastInCounter).TotalSeconds < 0.1f)
            return;

        _lastInCounter = DateTime.Now;
        Player_controller player = info.GetComponent<Player_controller>();

        if (player != null)
            player.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
