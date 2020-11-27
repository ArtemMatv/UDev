using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBaseController : Interactable
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _totalPrice;

    public int TotalPrice => _totalPrice;
    public string Description => _description; 
    public string Name => _name;

    protected virtual void Recreate() { }
}
