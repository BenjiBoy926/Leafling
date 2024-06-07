using Core;
using System;
using UnityEngine;

namespace Leafling
{
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
            float horizontalVelocity = body.GetVelocity(Dimension.X);
            bool exceedsNegativeVelocity = horizontalVelocity <= -_topSpeed;
            bool exceedsPositiveVelocity = horizontalVelocity >= _topSpeed;
            if (applyDirection < 0 && exceedsNegativeVelocity || applyDirection > 0 && exceedsPositiveVelocity)
            {
                return;
            }
            Vector2 force = _acceleration * applyDirection * Vector2.right;
            body.AddForce(force);
        }
    }
}