using Core;
using System;
using UnityEngine;

[Serializable]
public struct AirControl
{
    public float TopSpeed => _topSpeed;

    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _topSpeed;

    internal void ApplyTo(Rigidbody2D body, int applyDirection)
    {
        if (applyDirection == 0)
        {
            return;
        }
        if (WillPushExceedTopSpeed(body, applyDirection))
        {
            return;
        }
        Vector2 force = _acceleration * applyDirection * Vector2.right;
        body.AddForce(force);
        ClampVelocity(body);
    }
    // Codey: it is fine to enter this air control at a higher velocity than the 
    // top speed and let linear drag push the body back in range of the top speed
    private bool WillPushExceedTopSpeed(Rigidbody2D body, int applyDirection)
    {
        float horizontalVelocity = body.GetVelocity(Dimension.X);
        bool exceedsNegativeVelocity = horizontalVelocity <= -_topSpeed;
        bool exceedsPositiveVelocity = horizontalVelocity >= _topSpeed;
        return (applyDirection < 0 && exceedsNegativeVelocity) || (applyDirection > 0 && exceedsPositiveVelocity);
    }
    private void ClampVelocity(Rigidbody2D body)
    {
        float velocity = body.GetVelocity(Dimension.X);
        velocity = Mathf.Clamp(velocity, -_topSpeed, _topSpeed);
        body.SetVelocity(velocity, Dimension.X);
    }
}
