using Core;
using System;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public struct AirControl
    {
        [SerializeField]
        private float _acceleration;
        [SerializeField]
        private float _topSpeed;

        internal void ApplyTo(Rigidbody2D body, int applyDirection)
        {
            Vector2 force = _acceleration * applyDirection * Vector2.right;
            body.AddForce(force);
            float horizontalVelocity = body.GetVelocity(Dimension.X);
            horizontalVelocity = Mathf.Clamp(horizontalVelocity, -_topSpeed, _topSpeed);
            body.SetVelocity(horizontalVelocity, Dimension.X);
        }
    }
}