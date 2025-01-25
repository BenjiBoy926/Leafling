using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Branch : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private BoxCollider2D _collider;
    [SerializeField]
    private float _spriteWidth = 2;
    [SerializeField]
    private float _colliderWidth = 1.2f;

    private void Update()
    {
        if (!IsOperable())
        {
            return;
        }
        Sprite sprite = _renderer.sprite;
        Vector4 worldUnitBorder = sprite.border / sprite.pixelsPerUnit;
        Vector2 size = _collider.size;
        size.x = _renderer.size.x * (_colliderWidth / _spriteWidth);
        _collider.size = size;
    }
    private bool IsOperable()
    {
        return _renderer != null && _renderer.sprite != null && _collider != null;
    }
    private void Reset()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }
}
