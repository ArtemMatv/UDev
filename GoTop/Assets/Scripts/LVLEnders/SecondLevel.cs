using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevel : LvlEnder
{
    [SerializeField] private ChestController[] _chests;
    [SerializeField]  private int _amountToGoNext;
    private int _openedAmount;

    protected override void Start()
    {
        base.Start();
        
        foreach(ChestController chest in _chests)
        {
            chest.OnOpen += () => { 
                _openedAmount++;
                if (_openedAmount >= _amountToGoNext)
                    Open();
            };
        }
    }
}
