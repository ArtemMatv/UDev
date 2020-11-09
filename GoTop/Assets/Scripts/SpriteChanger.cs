using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _textures;
        
    public void ChangeTexture(int number = 0)
    {
        GetComponent<SpriteRenderer>().sprite = _textures[number];
    }
}
