using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteChanger))]
public class LvlEnder : MonoBehaviour
{
    private SpriteChanger _spriteChanger;
    private bool _opened = false;

    protected virtual void Start()
    {
        _spriteChanger = GetComponent<SpriteChanger>();
    }

    public virtual void Open()
    {
        _spriteChanger.ChangeTexture();
        _opened = true;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_opened)
            return;

        ServiceManager.Instance.EndLevel();
    }
}

