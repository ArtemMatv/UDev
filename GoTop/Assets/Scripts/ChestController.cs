using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteChanger))]
public class ChestController : MonoBehaviour
{
    public event Action OnOpen = delegate { };

    private SpriteChanger _spriteChanger;
    [SerializeField] private int _coins;
    private bool _opened = false;

    protected virtual void Start() 
    {
        _spriteChanger = GetComponent<SpriteChanger>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (_opened)
            return;

        _opened = true;
        _spriteChanger.ChangeTexture();
        collider.GetComponent<Player_controller>().AddCoins(_coins);
        OnOpen();
    }
}
