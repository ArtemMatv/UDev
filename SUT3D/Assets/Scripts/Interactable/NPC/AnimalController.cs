using System.Collections.Generic;
using UnityEngine;

public class AnimalController : NPCBaseController
{
    //you can not attack other npc
    [SerializeField] protected int _health;
    
    protected override void Move()
    {
        // move outside the village only
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }
}
