using UnityEngine;

public class LeaflingState_DropThrough : LeaflingState
{
    [SerializeField]
    private SpriteAnimation _animation;
    private Collider2D _platformCollider;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlatformEffector2D currentPlatform = Target.GetCurrentPlatform();
        if (currentPlatform)
        {
            _platformCollider = currentPlatform.GetComponent<Collider2D>();
            Target.SetAnimation(_animation);
            Target.IgnoreCollision(_platformCollider);
        }
        else
        {
            Target.SendSignal(new LeaflingSignal<LeaflingState_Crouch>());
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Target.RestoreCollision(_platformCollider);
    }

    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Normal));
    }
}