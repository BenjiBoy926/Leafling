using UnityEngine;

public static class TransformExtensions
{
    public static Transform SetLocalX(this Transform transform, float x)
    {
        transform.localPosition = transform.localPosition.SetX(x);
        return transform;
    }
}
