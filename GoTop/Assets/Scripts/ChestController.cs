using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField] private Sprite[] _openClosedTextures;
    [SerializeField] private int _coins;
    private bool _opened = false;

    protected virtual void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _openClosedTextures[0];
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (_opened)
            return;

        _opened = true;
        GetComponent<SpriteRenderer>().sprite = _openClosedTextures[1];
        collider.GetComponent<Player_controller>().AddCoins(_coins);
    }
}
