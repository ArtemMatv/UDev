using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevel : LvlEnder
{
    [SerializeField] private EnemyBossController _boss;

    protected override void Start()
    {
        base.Start();
        _boss.OnDeathAnswer += Open;
    }
}
