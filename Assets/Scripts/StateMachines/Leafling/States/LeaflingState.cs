using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaflingState : State<Leafling>
{
    protected bool HasEnteredActionFrame { get; private set;}

    protected override void OnEnable()
    {
        base.OnEnable();
        HasEnteredActionFrame = false;
        Target.HorizontalDirectionChanged += OnHorizontalDirectionChanged;
        Target.StartedJumping += OnStartedJumping;
        Target.StoppedJumping += OnStoppedJumping;
        Target.StartedAimingDash += OnStartedAimingDash;
        Target.StoppedAimingDash += OnStoppedAimingDash;
        Target.StartedCrouching += OnStartedCrouching;
        Target.StoppedCrouching += OnStoppedCrouching;
        Target.AnimationStarted += OnAnimationStarted;
        Target.AnimationFinished += OnAnimationFinished;
        Target.AnimationEnteredActionFrame += OnAnimationEnteredActionFrame;
        Target.DashTargetTouched += OnDashTargetTouched;
    }
    protected virtual void OnDisable()
    {
        Target.HorizontalDirectionChanged -= OnHorizontalDirectionChanged;
        Target.StartedJumping -= OnStartedJumping;
        Target.StoppedJumping -= OnStoppedJumping;
        Target.StartedAimingDash -= OnStartedAimingDash;
        Target.StoppedAimingDash -= OnStoppedAimingDash;
        Target.StartedCrouching -= OnStartedCrouching;
        Target.StoppedCrouching -= OnStoppedCrouching;
        Target.AnimationStarted -= OnAnimationStarted;
        Target.AnimationFinished -= OnAnimationFinished;
        Target.AnimationEnteredActionFrame -= OnAnimationEnteredActionFrame;
        Target.DashTargetTouched -= OnDashTargetTouched;
    }
    protected virtual void Update()
    {

    }

    protected virtual void OnHorizontalDirectionChanged()
    {

    }
    protected virtual void OnStartedJumping()
    {

    }
    protected virtual void OnStoppedJumping()
    {

    }
    protected virtual void OnStartedAimingDash()
    {

    }
    protected virtual void OnStoppedAimingDash()
    {

    }
    protected virtual void OnStartedCrouching()
    {

    }
    protected virtual void OnStoppedCrouching()
    {

    }
    protected virtual void OnAnimationStarted()
    {

    }
    protected virtual void OnAnimationFinished()
    {

    }
    protected virtual void OnAnimationEnteredActionFrame()
    {
        HasEnteredActionFrame = true;
    }
    protected virtual void OnDashTargetTouched(DashTarget target)
    {

    }
}
