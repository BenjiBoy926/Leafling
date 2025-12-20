using UnityEngine;

public class LeaflingState_WallSlide : LeaflingState
{
    [SerializeField]
    private float _animationTransitionScale = 0.25f;
    [SerializeField]
    private float _gravityScale = 0.1f;
    private CardinalDirection _wallDirection;

    public void SetWallDirection(CardinalDirection wallDirection)
    {
        _wallDirection = wallDirection;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bool flip = LeaflingStateTool_WallJump.WallDirectionToFlipX(_wallDirection);
        Target.SetTransition(new(Target.WallPerch, _animationTransitionScale, flip));
        Target.SetGravityScale(_gravityScale);
        Target.SetVerticalVelocity(0);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.ResetGravityScale();
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        Target.SendSignal(new LeaflingSignal_WallJump(_wallDirection));
    }

    protected override void Update()
    {
        base.Update();
        if (ShouldDisengage())
        {
            Disengage();
        }
        if (Target.IsTouching(CardinalDirection.Down) || !Target.IsTouching(_wallDirection))
        {
            Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Normal));
        }
    }
    private void Disengage()
    {
        Target.SetHorizontalVelocity(-_wallDirection.X * 5);
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
    }
    private bool ShouldDisengage()
    {
        return Target.HorizontalDirection != _wallDirection.X;
    }
}
