using System;
using System.Collections;
using UnityEngine;

public class LeaflingState_DashSpin : LeaflingState
{
    private Vector2 EndPosition => _target.Position;

    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private AnimationCurve _moveToTargetCurve;
    [SerializeField]
    private float _exitHop = 30;
    private DashTarget _target;
    private Vector2 _startPosition;

    public void SetTarget(DashTarget target)
    {
        _target = target;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.SetAnimation(_animation);
        Target.RestoreAbilityToDash();
        _startPosition = Target.GetPosition();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Target.SetVerticalVelocity(_exitHop);
        Target.FaceTowards(-Target.FacingDirection);
    }
    protected override void Update()
    {
        base.Update();
        float t = _moveToTargetCurve.Evaluate(Target.CurrentAnimationProgress);
        Vector2 position = Vector2.Lerp(_startPosition, EndPosition, t);
        Target.SetPosition(position);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
    }
}
