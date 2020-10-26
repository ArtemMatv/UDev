using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HorizontalMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;
    [SerializeField] private bool _flip;
    [SerializeField] private SpriteRenderer _renderer;
    private Vector2 _startPoint;
    private int _direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        _startPoint = transform.position;

        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((transform.position.x - _startPoint.x > _range) && (_direction > 0))
            || ((transform.position.x - _startPoint.x < -_range) && _direction < 0))
        {
            if (_flip)
                _renderer.flipX = !_renderer.flipX;
            
            _direction *= -1;

        }
        transform.Translate(_speed * _direction * Time.deltaTime, 0, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_range * 2, 0.5f, 0));
    }
}
