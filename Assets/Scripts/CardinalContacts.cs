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
        private float _overlapAreaExtents = 0.01f;
        [SerializeField]
        private float _overlapAreaMargin = 0.001f;
        private Contact[] _contacts = new Contact[4];

        private void Reset()
        {
            _collider = GetComponentInChildren<Collider2D>();
        }
        private void OnDrawGizmosSelected()
        {
            foreach (CardinalDirection direction in GetCardinalDirections())
            {
                DrawOverlapAreaGizmo(direction);
            }
        }
        private void DrawOverlapAreaGizmo(CardinalDirection direction)
        {
            if (_contacts[(int)direction].IsTouching)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawWireCube(GetAreaCenter(direction), GetAreaSize(direction));
        }

        public bool IsTouching(CardinalDirection direction)
        {
            return _contacts[(int)direction].IsTouching;
        }
        public bool IsTouchingAnything()
        {
            for (int i = 0; i < _contacts.Length; i++)
            {
                if (_contacts[i].IsTouching)
                {
                    return true;
                }
            }
            return false;
        }
        public IEnumerable<Vector2> GetContactNormals()
        {
            for (int i = 0; i < _contacts.Length; i++)
            {
                if (_contacts[i].IsTouching)
                {
                    yield return _contacts[i].Normal;
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _contacts.Length; i++)
            {
                UpdateContact(i);
            }
        }
        private void UpdateContact(int direction)
        {
            _contacts[direction] = BuildContact((CardinalDirection)direction);
        }
        private Contact BuildContact(CardinalDirection direction)
        {
            bool overlap = Physics2D.OverlapArea(GetAreaMin(direction), GetAreaMax(direction));
            return new Contact
            {
                IsTouching = overlap,
                Normal = direction.ToVector() * -1
            };
        }

        private CardinalDirection[] GetCardinalDirections()
        {
            return (CardinalDirection[])Enum.GetValues(typeof(CardinalDirection));
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
            return GetAreaCenter(direction.ToVector());
        }
        private Vector2 GetAreaExtents(CardinalDirection direction)
        {
            return GetAreaExtents(direction.ToVector());
        }
        private Vector2 GetAreaSize(CardinalDirection direction)
        {
            return GetAreaSize(direction.ToVector());
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
    }
}