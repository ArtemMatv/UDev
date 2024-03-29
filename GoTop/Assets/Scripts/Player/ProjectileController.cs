﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage;

    void OnTriggerEnter2D(Collider2D info)
    {
        EnemyControllerBase enemy = info.GetComponent<EnemyControllerBase>();

        if(enemy != null)
            enemy.TakeDamage(_damage, DamageTypes.Projectile);
            
        Destroy(gameObject);
    }
}
