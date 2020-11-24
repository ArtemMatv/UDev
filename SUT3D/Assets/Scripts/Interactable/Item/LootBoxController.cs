using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : ItemBaseController
{
    protected void Open()
    {
        //give loot to player
    }

    public override void Interact()
    {
        Open();
    }
}