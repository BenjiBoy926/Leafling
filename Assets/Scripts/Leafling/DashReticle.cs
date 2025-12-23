using UnityEngine;

public class DashReticle : MonoBehaviour
{
    public void ShowAim(Vector2 aim)
    {
        transform.up = aim;
    }
}
