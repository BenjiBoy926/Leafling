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

    public void SetWallDirection(CardinalDirection wallDirection)
    {
        _wallDirection = wallDirection;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_animation);
        Target.FaceTowards(LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection));
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
        if (Target.IsTouching(_wallDirection.Opposite))
        {
            Target.SendSignal(new LeaflingSignal_WallSlide(_wallDirection.Opposite, 0.5f));
        }
    }
    private bool ShouldFinishWallJump()
    {
        return !Target.IsTouching(_wallDirection) && (!Target.IsJumping || TimeSinceStateStart > _maxDuration);
    }
    private void FinishWallJump()
    {
        Target.SetVerticalVelocity(_exitHopSpeed);
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
    }
}
