using Core;
using System;
using UnityEngine;

[Serializable]
public struct AirControl
{
    private const float StationaryTolerance = 1f;

    public float TopSpeed => _topSpeed;

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _topSpeed;

    internal void ApplyTo(Rigidbody2D body, int applyDirection)
    {
        if (applyDirection != 0)
        {
            PushTowardsTopSpeed(body, applyDirection);
        }
        else
        {
            PushTowardsResting(body);
        }
    }

    private void PushTowardsTopSpeed(Rigidbody2D body, int direction)
    {
        if (WillPushExceedTopSpeed(body, direction))
        {
            return;
        }
        AddForce(body, direction);
    }
    private void PushTowardsResting(Rigidbody2D body)
    {
        if (!IsNearStationary(body))
        {
            AddForce(body, GetRestingForceDirection(body));
        }
        else if (!IsStationary(body))
        {
            body.SetVelocity(0, Dimension.X);
        }
    }

    private void AddForce(Rigidbody2D body, int direction)
    {
        Vector2 force = _acceleration * direction * Vector2.right;
        body.AddForce(force);
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
    private bool IsNearStationary(Rigidbody2D body)
    {
        float velocity = body.GetVelocity(Dimension.X);
        return Mathf.Abs(velocity) < StationaryTolerance;
    }
    private bool IsStationary(Rigidbody2D body)
    {
        float velocity = body.GetVelocity(Dimension.X);
        return Mathf.Approximately(velocity, 0);
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
}
