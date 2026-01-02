using UnityEngine;

public class LeaflingState_Crouch : LeaflingState
{
    [SerializeField]
    private float _doubleTapInterval = 0.3f;
    private float _timeOfPreviousCrouchStart;

    protected override void OnEnable()
    {
        _timeOfPreviousCrouchStart = TimeOfStateStart;
        base.OnEnable();
        Target.SetAnimation(Target.Squat);
        if (DoubleCrouchDetected() && Target.GetCurrentPlatform())
        {
            Target.SetState<LeaflingState_DropThrough>();
        }
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        PlatformEffector2D platform = Target.GetCurrentPlatform();
        if (platform)
        {
            Target.SetState<LeaflingState_DropThrough>();
        }
        else
        {
            Target.SetState<LeaflingState_CrouchJump>();
        }
    }
    protected override void OnStoppedCrouching()
    {
        base.OnStoppedCrouching();
        Target.SetState<LeaflingState_Standing>();
    }

    private bool DoubleCrouchDetected()
    {
        return !Mathf.Approximately(TimeOfStateStart, 0) && TimeOfStateStart - _timeOfPreviousCrouchStart <= _doubleTapInterval;
    }
}
