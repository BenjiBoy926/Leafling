using Core;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CardinalContacts : MonoBehaviour
{
    private Vector2 RaycastMargin => new(_raycastSideMargin, RaycastVerticalMargin);
    private float RaycastVerticalMargin => _raycastLength * 0.1f;
    private Vector2 ColliderCenter => _collider.bounds.center;
    private Vector2 ColliderExtents => _collider.bounds.extents;
    public Collider2D Collider => _collider;

    [SerializeField]
    private Collider2D _collider;
    [SerializeField]
    private LayerMask _downContactMask;
    [SerializeField]
    private LayerMask _leftRightUpContactMask;
    [SerializeField]
    private float _raycastLength = 0.01f;
    [SerializeField]
    private float _raycastSideMargin = 0.01f;
    private readonly Dictionary<CardinalDirection, Contact> _contacts = new()
    {
        [CardinalDirection.Up] = new Contact(),
        [CardinalDirection.Down] = new Contact(),
        [CardinalDirection.Left] = new Contact(),
        [CardinalDirection.Right] = new Contact(),
    };

    private void Reset()
    {
        _collider = GetComponentInChildren<Collider2D>();
    }

    public bool IsTouching(CardinalDirection direction)
    {
        return _contacts[direction].IsTouching;
    }
    public bool IsTouchingAnything()
    {
        for (int i = 0; i < CardinalDirection.Count; i++)
        {
            if (_contacts[CardinalDirection.Get(i)].IsTouching)
            {
                return true;
            }
        }
        return false;
    }
    public Contact GetContact(CardinalDirection direction)
    {
        return _contacts[direction];
    }
    public IEnumerable<Vector2> GetContactNormals()
    {
        for (int i = 0; i < CardinalDirection.Count; i++)
        {
            Contact contact = _contacts[CardinalDirection.Get(i)];
            if (contact.IsTouching)
            {
                yield return contact.Normal;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < CardinalDirection.Count; i++)
        {
            UpdateContact(CardinalDirection.Get(i));
        }
    }
    private void UpdateContact(CardinalDirection direction)
    {
        _contacts[direction] = BuildContact(direction);
    }
    private Contact BuildContact(CardinalDirection direction)
    {
        LayerMask mask = direction == CardinalDirection.Down ? _downContactMask : _leftRightUpContactMask;
        RaycastHit2D min = Physics2D.Raycast(GetMinOrigin(direction), direction.FloatVector, _raycastLength, mask);
        RaycastHit2D max = Physics2D.Raycast(GetMaxOrigin(direction), direction.FloatVector, _raycastLength, mask);
        return new Contact
        {
            Normal = direction.Vector * -1,
            Min = min,
            Max = max
        };
    }

    private Vector2 GetMinOrigin(CardinalDirection direction)
    {
        return new(GetMinOriginX(direction), GetMinOriginY(direction));
    }
    private float GetMinOriginX(CardinalDirection direction)
    {
        return GetMinOriginDimension(direction, Dimension.X);
    }
    private float GetMinOriginY(CardinalDirection direction)
    {
        return GetMinOriginDimension(direction, Dimension.Y);
    }
    private float GetMinOriginDimension(CardinalDirection direction, Dimension dimension)
    {
        float sign = direction.Dimension(dimension) == 1 ? -1 : 1;
        float center = ColliderCenter.Get(dimension);
        float extent = ColliderExtents.Get(dimension) * sign;
        float margin = RaycastMargin.Get(dimension) * sign;
        return center - extent + margin;
    }
    private Vector2 GetMaxOrigin(CardinalDirection direction)
    {
        return new(GetMaxOriginX(direction), GetMaxOriginY(direction));
    }
    private float GetMaxOriginX(CardinalDirection direction)
    {
        return GetMaxOriginDimension(direction, Dimension.X);
    }
    private float GetMaxOriginY(CardinalDirection direction)
    {
        return GetMaxOriginDimension(direction, Dimension.Y);
    }
    private float GetMaxOriginDimension(CardinalDirection direction, Dimension dimension)
    {
        float sign = direction.Dimension(dimension) == -1 ? -1 : 1;
        float center = ColliderCenter.Get(dimension);
        float extent = ColliderExtents.Get(dimension) * sign;
        float margin = RaycastMargin.Get(dimension) * sign;
        return center + extent - margin;
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < CardinalDirection.Count; i++)
        {
            DrawRays(CardinalDirection.Get(i));
        }
    }
    private void DrawRays(CardinalDirection direction)
    {
        if (_contacts[direction].IsTouching)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.yellow;
        }
        Gizmos.DrawRay(GetMinOrigin(direction), direction.FloatVector * _raycastLength);
        Gizmos.DrawRay(GetMaxOrigin(direction), direction.FloatVector * _raycastLength);
    }
}