using UnityEngine;

public class LeaflingState_DropThrough : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    [SerializeField]
    private float _dropDistance = 3;
    [SerializeField]
    private AnimationCurve _dropSpeedFalloff;
    private Collider2D _platformCollider;
    private float _dropSpeed;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlatformEffector2D currentPlatform = Target.GetCurrentPlatform();
        if (currentPlatform)
        {
            _platformCollider = currentPlatform.GetComponent<Collider2D>();
           _dropSpeed = CalculateDropSpeed();
            Target.SetAnimation(_animation);
            Target.IgnoreCollision(_platformCollider);
        }
        else
        {
            Target.SetState<LeaflingState_Crouch>();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Target.RestoreCollision(_platformCollider);
    }

    protected override void Update()
    {
        base.Update();
        float dropSpeed = _dropSpeed;
        if (!Target.IsCurrentFrameActionFrame)
        {
            float percent = _dropSpeedFalloff.Evaluate(Target.ProgressAfterFirstActionFrame);
            dropSpeed *= percent;
        }
        Target.SetVerticalVelocity(-dropSpeed);
    }

    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Normal);
    }

    private float CalculateDropSpeed()
    {
        SpriteAnimationFrame firstFrame = _animation.GetFrame(0);
        float duration = firstFrame.Duration;
        return _dropDistance / duration;
    }
}