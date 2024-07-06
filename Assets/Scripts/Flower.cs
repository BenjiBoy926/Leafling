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

    private void OnTickled()
    {
        Tickled();
    }
    private void OnStruck()
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
    public void SendSignal(ISignal<Flower> signal)
    {
        _stateMachine.SendSignal(signal);
    }
}
