using UnityEngine;

namespace Core
{
    public static class Vector2IntExtensions
    {
        public static int Get(this Vector2Int vector, Dimension dimension)
        {
            return vector[(int)dimension];
        }
        public static Vector2Int Set(this Vector2Int vector, int value, Dimension dimension)
        {
            vector[(int)dimension] = value;
            return vector;
        }
    }
}