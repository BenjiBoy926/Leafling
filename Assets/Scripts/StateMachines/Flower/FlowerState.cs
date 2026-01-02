using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerState : State<Flower>
{
    [SerializeField]
    private SpriteAnimation _idleAnimation;
    [SerializeField]
    private SpriteAnimation _tickleAnimation;
    [SerializeField]
    private SpriteAnimation _strikeAnimation;

    protected override void OnEnable()
    {
        base.OnEnable();
        Target.Tickled += OnFlowerTickled;
        Target.Struck += OnFlowerStruck;
        Target.AnimationFinished += OnAnimationFinished;
    }
    private void OnDisable()
    {
        Target.Tickled -= OnFlowerTickled;
        Target.Struck -= OnFlowerStruck;
        Target.AnimationFinished -= OnAnimationFinished;
    }

    protected virtual void OnFlowerTickled()
    {
        Target.SetAnimation(_tickleAnimation);
    }
    protected virtual void OnFlowerStruck()
    {
        Target.SetAnimation(_strikeAnimation);
    }
    private void OnAnimationFinished()
    {
        Target.SetAnimation(_idleAnimation);
    }
}
