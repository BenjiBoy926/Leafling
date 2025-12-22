using UnityEngine;

public struct Contact
{
    public bool IsTouching => Min || Max;

    public Vector2 Normal;
    public RaycastHit2D Min;
    public RaycastHit2D Max;
}
