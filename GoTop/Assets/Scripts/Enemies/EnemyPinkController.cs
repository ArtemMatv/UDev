using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPinkController : EnemyControllerBase
{
    public override void TakeDamage(int damage, DamageTypes type = DamageTypes.Casual, Transform player = null)
    {
        if (type != DamageTypes.Projectile)
            return;

        base.TakeDamage(damage, type, player);
    }
}
