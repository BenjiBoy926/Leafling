using UnityEngine;

namespace Core
{
    public static class Rigidbody2DExtensions
    {
        public static float GetVelocity(this Rigidbody2D rigidbody, Dimension dimension)
        {
            return rigidbody.velocity.Get(dimension);
        }
        public static void SetVelocity(this Rigidbody2D rigidbody, float velocity, Dimension dimension)
        {
            rigidbody.velocity = rigidbody.velocity.Set(velocity, dimension);
        }
    }
}