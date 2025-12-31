using Core;
using System;
using UnityEngine;

[Serializable]
public struct AirControl
{
    private const float StationaryTolerance = 0.001f;

    public float TopSpeed => _topSpeed;

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _topSpeed;

    internal void ApplyTo(Rigidbody2D body, int applyDirection)
    {
        if (WillPushExceedTopSpeed(body, applyDirection))
        {
            return;
        }
        int forceDirection = GetForceDirection(body, applyDirection);
        Vector2 force = _acceleration * forceDirection * Vector2.right;
        body.AddForce(force);
        if (applyDirection != 0)
        {
            ClampVelocity(body);
        }
    }
    // Codey: it is fine to enter this air control at a higher velocity than the 
    // top speed and let linear drag push the body back in range of the top speed
    private bool WillPushExceedTopSpeed(Rigidbody2D body, int applyDirection)
    {
        if (applyDirection == 0) return false;

        float horizontalVelocity = body.GetVelocity(Dimension.X);
        bool exceedsNegativeVelocity = horizontalVelocity <= -_topSpeed;
        bool exceedsPositiveVelocity = horizontalVelocity >= _topSpeed;
        return (applyDirection < 0 && exceedsNegativeVelocity) || (applyDirection > 0 && exceedsPositiveVelocity);
    }
    private int GetForceDirection(Rigidbody2D body, int applyDirection)
    {
        return applyDirection == 0 ? GetRestingForceDirection(body) : applyDirection;
    }
    private int GetRestingForceDirection(Rigidbody2D body)
    {
        float xVelocity = body.GetVelocity(Dimension.X);
        if (xVelocity > StationaryTolerance)
        {
            return -1;
        }
        else if (xVelocity < StationaryTolerance)
        {
            return 1;
        }
        return 0;
    }
    private void ClampVelocity(Rigidbody2D body)
    {
        float velocity = body.GetVelocity(Dimension.X);
        velocity = Mathf.Clamp(velocity, -_topSpeed, _topSpeed);
        body.SetVelocity(velocity, Dimension.X);
    }
}
