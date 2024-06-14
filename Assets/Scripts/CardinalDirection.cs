using System;
using UnityEngine;

namespace Leafling
{
    public abstract class CardinalDirection
    {
        public static CardinalDirection Left = new CardinalDirection_Left();
        public static CardinalDirection Right = new CardinalDirection_Right();
        public static CardinalDirection Up = new CardinalDirection_Up();
        public static CardinalDirection Down = new CardinalDirection_Down();
        public static CardinalDirection[] All = new CardinalDirection[] { Left, Right, Up, Down };
        public static int Count = All.Length;
        public static CardinalDirection Get(int index)
        {
            return All[index];
        }

        protected CardinalDirection() { }

        public float X => Vector.x;
        public float Y => Vector.y;
        public abstract Vector2 Vector { get; }
        public abstract CardinalDirection Opposite { get; }
    }

    public class CardinalDirection_Left : CardinalDirection
    {
        public override Vector2 Vector => Vector2.left;
        public override CardinalDirection Opposite => new CardinalDirection_Right();
    }
    public class CardinalDirection_Right : CardinalDirection
    {
        public override Vector2 Vector => Vector2.right;
        public override CardinalDirection Opposite => new CardinalDirection_Left();
    }

    public class CardinalDirection_Up : CardinalDirection
    {
        public override Vector2 Vector => Vector2.up;
        public override CardinalDirection Opposite => new CardinalDirection_Down();
    }
    public class CardinalDirection_Down : CardinalDirection
    {
        public override Vector2 Vector => Vector2.down;
        public override CardinalDirection Opposite => new CardinalDirection_Up();
    }
}