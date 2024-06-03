using UnityEngine;

namespace Core
{
    public static class VectorExtensions
    {
        public static float Get(this Vector2 vector, Dimension dimension)
        {
            return vector[(int)dimension];
        }
        public static Vector2 Set(this Vector2 vector, float value, Dimension dimension)
        {
            vector[(int)dimension] = value;
            return vector;
        }
        public static Vector2 Multiply(this Vector2 vector, Vector2 other)
        {
            return new(vector.x * other.x, vector.y * other.y);
        }
        public static Vector2 Abs(this Vector2 vector)
        {
            return new(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
        public static Vector2 SwizzleYX(this Vector2 vector)
        {
            return new(vector.y, vector.x);
        }
    }
}