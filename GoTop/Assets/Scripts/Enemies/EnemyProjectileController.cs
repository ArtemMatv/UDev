using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage;

    void OnTriggerEnter2D(Collider2D info)
    {
        Player_controller player = info.GetComponent<Player_controller>();

        if (player != null)
            player.ChangeHp(-_damage);

        Destroy(gameObject);
    }
}
