using Core;
using System;
using UnityEngine;

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

    public Vector2 FloatVector => Vector;
    public int X => Vector.x;
    public int Y => Vector.y;
    public abstract Vector2Int Vector { get; }
    public abstract CardinalDirection Opposite { get; }

    public int Dimension(Dimension dimension)
    {
        return Vector.Get(dimension);
    }
}

public class CardinalDirection_Left : CardinalDirection
{
    public override Vector2Int Vector => Vector2Int.left;
    public override CardinalDirection Opposite => Right;
}
public class CardinalDirection_Right : CardinalDirection
{
    public override Vector2Int Vector => Vector2Int.right;
    public override CardinalDirection Opposite => Left;
}

public class CardinalDirection_Up : CardinalDirection
{
    public override Vector2Int Vector => Vector2Int.up;
    public override CardinalDirection Opposite => Down;
}
public class CardinalDirection_Down : CardinalDirection
{
    public override Vector2Int Vector => Vector2Int.down;
    public override CardinalDirection Opposite => Up;
}