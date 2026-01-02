using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

public class LeaflingState_TargetStrike : LeaflingState
{
    [SerializeField]
    private float _duration = 0.2f;
    [SerializeField, ReadOnly]
    private DashTarget _target;

    protected override void OnEnable()
    {
        base.OnEnable();
        SpriteAnimator animator = Target.Animator;
        Transform spriteTransform = animator.transform;

        spriteTransform.DOComplete();
    }
}
