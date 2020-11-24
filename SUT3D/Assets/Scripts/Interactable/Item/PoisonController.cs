using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : ItemBaseController
{
    public PoisonController(string _name)
    {
        this.Description = "Deal damage to player or NPC";
        this.Name = _name;
        this.Type = "Harmful";
        this.Reusable = false;
    }

    protected override void Recreate()
    {
        // refill poison
    }

    public override void Interact()
    {
        // Deal damage to object if isn't empty
    }
}
