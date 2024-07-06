using System;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(Leafling) + "/" + nameof(DirectionalAirControl))]
public class DirectionalAirControl : ScriptableObject
{
    public float ForwardTopSpeed => _forwards.TopSpeed;

    [SerializeField]
    private AirControl _forwards;
    [SerializeField]
    private AirControl _backwards;

    public void ApplyTo(Rigidbody2D body, int applyDirection, int facingDirection)
    {
        if (applyDirection == 0)
        {
            return;
        }
        else if (applyDirection == facingDirection)
        {
            _forwards.ApplyTo(body, applyDirection);
        }
        else
        {
            _backwards.ApplyTo(body, applyDirection);
        }
    }
}
