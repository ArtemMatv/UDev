using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseController : Interactable
{
    protected string Name { get; set; }
    protected string Role { get; set; }
    protected int Health { get; set; }
    protected List<string> Messages { get; set; }

    protected virtual void Move() { }

    protected virtual void Talk() { }
}
