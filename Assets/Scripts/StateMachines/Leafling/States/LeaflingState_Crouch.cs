using UnityEngine;

public class LeaflingState_Crouch : LeaflingState
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(Target.Squat);
    }
    protected override void OnStartedJumping()
    {
        base.OnStartedJumping();
        PlatformEffector2D platform = Target.GetCurrentPlatform();
        if (platform)
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_DropThrough>());
        }
        else
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_CrouchJump>());
        }
    }
    protected override void OnStoppedCrouching()
    {
        base.OnStoppedCrouching();
        Target.SendSignal(new LeaflingSignal<LeaflingState_Standing>());
    }
}
