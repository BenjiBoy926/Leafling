using UnityEngine;

[CreateAssetMenu(menuName = nameof(Leafling) + "/" + nameof(DirectionalPhysicsControl))]
public class DirectionalPhysicsControl : ScriptableObject
{
    public float ForwardTopSpeed => _forwards.TopSpeed;

    [SerializeField]
    private PhysicsControl _forwards;
    [SerializeField]
    private PhysicsControl _backwards;

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
