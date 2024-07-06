using UnityEngine;

public class LeaflingState_JumpSquat : LeaflingState
{
    [SerializeField]
    private float _animationTransitionScale = 0.1f;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetTransition(new(Target.Squat, _animationTransitionScale, Target.CurrentFlipX));
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
