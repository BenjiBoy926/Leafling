using UnityEngine;

public class LeaflingState_JumpSquat : LeaflingState
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(Target.Squat);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        if (Target.IsAnimating(Target.Squat))
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Jump>());
        }
    }
}
