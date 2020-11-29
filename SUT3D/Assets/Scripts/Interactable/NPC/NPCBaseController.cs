using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseController : Interactable
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _role;
    [SerializeField] protected List<string> _messages;

    public string Role => _role;
    public List<string> Messages => _messages;
    public string Name => _name;

    protected virtual void Move() { }

    protected virtual void Talk() { }
}
