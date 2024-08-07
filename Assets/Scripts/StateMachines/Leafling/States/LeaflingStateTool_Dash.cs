using System.Collections.Generic;
using UnityEngine;

public class LeaflingStateTool_Dash
{
    public static void ShowDashPerch(Leafling leafling, Vector2 aim)
    {
        TransitionDashPerch(leafling, 0, aim);
    }
    public static void TransitionDashPerch(Leafling leafling, float scale, Vector2 aim)
    {
        SpriteAnimation animation = GetPerchAnimation(leafling);
        bool flipX = GetPerchFlipX(leafling, aim.x);
        leafling.SetTransition(new(animation, scale, flipX));
        SetRotation(leafling, aim);
    }

    private static void SetRotation(Leafling leafling, Vector2 aim)
    {
        if (leafling.IsTouchingAnything())
        {
            leafling.ResetSpriteRotation();
        }
        else
        {
            SetMidairRotation(leafling, aim);
        }
    }
    public static void SetMidairRotation(Leafling leafling, Vector2 aim)
    {
        Vector2 spriteRight = aim;
        if (spriteRight.x < 0)
        {
            spriteRight *= -1;
        }
        leafling.SetSpriteRight(spriteRight);
    }
    private static SpriteAnimation GetPerchAnimation(Leafling leafling)
    {
        if (leafling.IsTouching(CardinalDirection.Down))
        {
            return leafling.Squat;
        }
        if (leafling.IsTouching(CardinalDirection.Up))
        {
            return leafling.CeilingPerch;
        }
        if (leafling.IsTouching(CardinalDirection.Right) || leafling.IsTouching(CardinalDirection.Left))
        {
            return leafling.WallPerch;
        }
        return leafling.MidairDashAim;
    }
    private static bool GetPerchFlipX(Leafling leafling, float aimX)
    {
        return Leafling.DirectionToFlipX(GetPerchDirection(leafling, aimX));
    }
    private static int GetPerchDirection(Leafling leafling, float aimX)
    {
        if (leafling.IsTouching(CardinalDirection.Right))
        {
            return -1;
        }
        if (leafling.IsTouching(CardinalDirection.Left))
        {
            return 1;
        }
        if (aimX < 0)
        {
            return -1;
        }
        return 1;
    }

    public static Vector2 ClampDashAim(Leafling leafling, Vector2 aim)
    {
        foreach (Vector2 normal in leafling.GetContactNormals())
        {
            aim = ClampAbovePlane(aim, normal);
        }
        return aim;
    }
    private static Vector2 ClampAbovePlane(Vector2 vector, Vector2 normal)
    {
        if (Vector2.Dot(vector, normal) < 0)
        {
            return Vector3.ProjectOnPlane(vector, normal).normalized;
        }
        return vector;
    }
}
