using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 SetX(this Vector3 vector, float x)
    {
        Vector3 result = vector;
        result.x = x;
        return result;
    }
}
