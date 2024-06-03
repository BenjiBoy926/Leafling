using System;
using UnityEngine;

namespace Leafling
{
    public enum CardinalDirection
    {
        Up, Down, Left, Right
    }
    public static class CardinalDirectionExtensions
    {
        public static Vector2 ToVector(this CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.Up: return Vector2.up;
                case CardinalDirection.Down: return Vector2.down;
                case CardinalDirection.Left: return Vector2.left;
                case CardinalDirection.Right: return Vector2.right;
                default: throw new ArgumentException($"Unexpected {nameof(CardinalDirection)} '{direction}'");
            }
        }
    }
}