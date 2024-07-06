using System;
using UnityEngine;

public class LeaflingInputs : MonoBehaviour
{
    public event Action HorizontalDirectionChanged = delegate { };
    public event Action StartedJumping = delegate { };
    public event Action StoppedJumping = delegate { };
    public event Action StartedAimingDash = delegate { };
    public event Action StoppedAimingDash = delegate { };
    public event Action StartedCrouching = delegate { };
    public event Action StoppedCrouching = delegate { };

    public int HorizontalDirection => _horizontalDirection;
    public bool IsJumping => _isJumping;
    public bool IsAimingDash => _isAimingDash;
    public Vector2 DashAim => _dashAim;
    public float DashAimX => _dashAim.x;
    public float DashAimY => _dashAim.y;
    public bool IsCrouching => _isCrouching;

    [SerializeField, Range(-1, 1)]
    private int _horizontalDirection = 0;
    [SerializeField]
    private bool _isJumping;
    [SerializeField]
    private Vector2 _dashAim;
    [SerializeField]
    private bool _isAimingDash;
    [SerializeField]
    private bool _isCrouching;

    public void SetHorizontalDirection(int horizontalDirection)
    {
        if (_horizontalDirection == horizontalDirection)
        {
            return;
        }
        _horizontalDirection = horizontalDirection;
        HorizontalDirectionChanged();
    }
    public void SetIsJumping(bool isJumping)
    {
        if (isJumping == _isJumping)
        {
            return;
        }
        _isJumping = isJumping;
        if (isJumping)
        {
            StartedJumping();
        }
        else
        {
            StoppedJumping();
        }
    }
    public void SetDashTarget(Vector2 target)
    {
        SetDashAim(target - (Vector2)transform.position);
    }
    public void SetDashAim(Vector2 direction)
    {
        direction = MakeVectorIntoDashDirection(direction);
        if (direction == _dashAim)
        {
            return;
        }
        _dashAim = direction;
    }
    public void SetIsAimingDash(bool isAimingDash)
    {
        if (isAimingDash == _isAimingDash)
        {
            return;
        }
        _isAimingDash = isAimingDash;
        if (isAimingDash)
        {
            StartedAimingDash();
        }
        else
        {
            StoppedAimingDash();
        }
    }
    public void SetIsCrouching(bool isCrouching)
    {
        if (isCrouching == _isCrouching)
        {
            return;
        }
        _isCrouching = isCrouching;
        if (isCrouching)
        {
            StartedCrouching();
        }
        else
        {
            StoppedCrouching();
        }
    }
    private Vector2 MakeVectorIntoDashDirection(Vector2 vector)
    {
        return vector.normalized;
    }
}
