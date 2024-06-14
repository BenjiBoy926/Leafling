using Core;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class CardinalContacts : MonoBehaviour
    {
        private Vector2 ColliderCenter => _collider.bounds.center;
        private Vector2 ColliderExtents => _collider.bounds.extents;
        private Vector2 ColliderSize => _collider.bounds.size;
        private Vector2 OverlapAreaExtentsVector => Vector2.one * _overlapAreaExtents;
        private Vector2 OverlapAreaMarginVector => Vector2.one * _overlapAreaMargin;
        private Vector2 OverlapAreaSizeVector => OverlapAreaExtentsVector * 2;

        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private float _raycastLength = 0.01f;
        [SerializeField]
        private float _overlapAreaExtents = 0.01f;
        [SerializeField]
        private float _overlapAreaMargin = 0.001f;
        private Dictionary<CardinalDirection, Contact> _contacts = new()
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
            bool overlap = Physics2D.OverlapArea(GetAreaMin(direction), GetAreaMax(direction));
            return new Contact
            {
                IsTouching = overlap,
                Normal = direction.Vector * -1
            };
        }

        private Vector2 GetAreaMax(CardinalDirection direction)
        {
            return GetAreaCenter(direction) + GetAreaExtents(direction);
        }
        private Vector2 GetAreaMin(CardinalDirection direction)
        {
            return GetAreaCenter(direction) - GetAreaExtents(direction);
        }
        private Vector2 GetAreaCenter(CardinalDirection direction)
        {
            return GetAreaCenter(direction.Vector);
        }
        private Vector2 GetAreaExtents(CardinalDirection direction)
        {
            return GetAreaExtents(direction.Vector);
        }
        private Vector2 GetAreaSize(CardinalDirection direction)
        {
            return GetAreaSize(direction.Vector);
        }

        private Vector2 GetAreaCenter(Vector2 direction)
        {
            direction = (OverlapAreaExtentsVector + OverlapAreaMarginVector + ColliderExtents) * direction;
            return ColliderCenter + direction;
        }
        private Vector2 GetAreaExtents(Vector2 direction)
        {
            return GetAreaSize(direction) * 0.5f;
        }
        private Vector2 GetAreaSize(Vector2 direction)
        {
            direction = direction.Abs();
            Vector2 parallelSize = OverlapAreaSizeVector;
            Vector2 perpendicularSize = ColliderSize - 2 * _overlapAreaMargin * Vector2.one;
            return parallelSize * direction + perpendicularSize * direction.SwizzleYX();
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < CardinalDirection.Count; i++)
            {
                DrawOverlapAreaGizmo(CardinalDirection.Get(i));
            }
        }
        private void DrawOverlapAreaGizmo(CardinalDirection direction)
        {
            if (_contacts[direction].IsTouching)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawWireCube(GetAreaCenter(direction), GetAreaSize(direction));
        }
    }
}