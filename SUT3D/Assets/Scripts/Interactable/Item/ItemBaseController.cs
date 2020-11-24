using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBaseController : Interactable
{
    protected string Name { get; set; }
    protected string Type { get; set; }
    protected string Description { get; set; }
    protected bool Reusable { get; set; } = false;
    protected int Usages { get; set; } = 1;

    protected virtual void Recreate() { }
}
