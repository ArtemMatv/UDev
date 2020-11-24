using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : LootBoxController
{
    private List<ItemBaseController> Items { get; set; }

    public CrateController(string _name)
    {
        this.Name = _name;
        this.Type = "LootBox";
        this.Description = "Crate with loot";
        Items = new List<ItemBaseController> { new PoisonController("Derfer"), new HealController("Song of angles") };
    }

    public override void Interact()
    {
        base.Interact();
        //Open crate for loot
    }
}
