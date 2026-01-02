using System;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public event Action Tickled = delegate { };
    public event Action Struck = delegate { };
    public event Action AnimationFinished = delegate { };

    [SerializeField]
    private DashTarget _target;
    [SerializeField]
    private SpriteAnimator _animator;
    [SerializeField]
    private FlowerStateMachine _stateMachine;
    [SerializeField]
    private float _pushSpeed = 10;

    private void OnEnable()
    {
        _target.Tickled += OnTickled;
        _target.Struck += OnStruck;
        _animator.FinishedAnimation += OnAnimationFinished;
    }
    private void OnDisable()
    {
        _target.Tickled -= OnTickled;
        _target.Struck -= OnStruck;
        _animator.FinishedAnimation -= OnAnimationFinished;
    }

    private void OnTickled(DashTargeter targeter)
    {
        if (targeter.TryGetComponent(out Leafling leafling))
        {
            PushLeaflingAway(leafling);
        }
        Tickled();
    }
    private void OnStruck(DashTargeter targeter)
    {
        Struck();
    }
    private void OnAnimationFinished()
    {
        AnimationFinished();
    }

    public void SetAnimation(SpriteAnimation animation)
    {
        _animator.SetAnimation(animation);
    }
    public void SetState<TState>() where TState : FlowerState
    {
        _stateMachine.SetState<TState>();
    }

    private void PushLeaflingAway(Leafling leafling)
    {
        Vector2 position = transform.position;
        Vector2 pushDirection = leafling.GetPosition() - position;
        pushDirection.y = 0;
        pushDirection = pushDirection.normalized;
        leafling.SetVelocity(pushDirection * _pushSpeed);
    }
}
