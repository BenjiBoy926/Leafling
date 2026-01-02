using UnityEngine;

public class LeaflingState_WallJump : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _speed = 30;
    [SerializeField]
    private float _exitHopSpeed = 20;
    [SerializeField]
    private float _maxDuration = 1;
    private CardinalDirection _wallDirection;

    public static void Enter(Leafling target, CardinalDirection wallDirection)
    {
        LeaflingState_WallJump state = target.GetState<LeaflingState_WallJump>();
        state._wallDirection = wallDirection;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_animation);
        Target.FaceTowards(LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection));
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        target.Tickle(Target.DashTargeter);
    }
    protected override void Update()
    {
        base.Update();
        float direction = LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection);
        Vector2 velocity = new Vector2(direction, 1).normalized * _speed;
        Target.SetVelocity(velocity);
        if (ShouldFinishWallJump())
        {
            FinishWallJump();
        }
    }
    private bool ShouldFinishWallJump()
    {
        return !Target.IsTouching(_wallDirection) && (!Target.IsJumping || TimeSinceStateStart > _maxDuration);
    }
    private void FinishWallJump()
    {
        Target.SetVerticalVelocity(_exitHopSpeed);
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
    }
}
