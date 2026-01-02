using UnityEngine;

public class LeaflingState_WallSlide : LeaflingState
{
    [SerializeField]
    private float _gravityScale = 0.1f;
    [SerializeField]
    private float _releaseGracePeriod = 0.3f;
    private float _timeOfInputAwayFromWall;
    private CardinalDirection _wallDirection;

    public static void Enter(Leafling target, CardinalDirection wallDirection)
    {
        LeaflingState_WallSlide state = target.GetState<LeaflingState_WallSlide>();
        state._wallDirection = wallDirection;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        int direction = LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection);
        Target.FaceTowards(direction);
        Target.SetAnimation(Target.WallPerch);
        Target.SetGravityScale(_gravityScale);
        Target.SetVerticalVelocity(0);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.ResetGravityScale();
    }
    protected override void OnHorizontalDirectionChanged()
    {
        base.OnHorizontalDirectionChanged();
        if (IsInputAwayFromWall())
        {
            _timeOfInputAwayFromWall = Time.time;
        }
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        LeaflingState_WallJump.Enter(Target, _wallDirection);
    }
    protected override void OnStartedAimingDash()
    {
        base.OnStartedAimingDash();
        Target.SetState<LeaflingState_DashAim>();
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
            LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Normal);
        }
    }
    private void Disengage()
    {
        Target.SetHorizontalVelocity(-_wallDirection.X * 5);
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
    }
    private bool ShouldDisengage()
    {
        float timeSinceInputAwayFromWall = Time.time - _timeOfInputAwayFromWall;
        return IsInputAwayFromWall() && timeSinceInputAwayFromWall > _releaseGracePeriod;
    }
    private bool IsInputAwayFromWall()
    {
        return Target.HorizontalDirection != _wallDirection.X;
    }
}
