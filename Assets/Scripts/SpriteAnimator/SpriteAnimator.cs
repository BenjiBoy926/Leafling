using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public event Action StartedAnimation = delegate { };
    public event Action FinishedAnimation = delegate { };
    public event Action ActionFrameEntered = delegate { };

    private bool IsCurrentFrameFinished => TimeSinceCurrentFrameStart >= CurrentFrameDuration;
    private float CurrentFrameDuration => IsTransitioning() ? CurrentFrame.Duration * _activeTransition.Scale : CurrentFrame.Duration;
    private float TimeSinceCurrentFrameStart => Time.time - _currentFrameStartTime;
    private float TimeSinceCurrentAnimationStart => Time.time - _currentAnimationStartTime;
    public float CurrentFrameProgress => TimeSinceCurrentFrameStart / CurrentFrame.Duration;
    public float CurrentAnimationProgress => TimeSinceCurrentAnimationStart / _currentAnimation.Duration;
    public bool FlipX => _body.FlipX;
    private SpriteAnimationFrame CurrentFrame => _currentAnimation.GetFrame(_currentFrameIndex);
    private SpriteAnimationFrame PreviousFrame => _currentAnimation.GetFrame(_currentFrameIndex + 1);
    private SpriteAnimationFrame NextFrame => _currentAnimation.GetFrame(_currentFrameIndex - 1);
    public bool IsCurrentFrameActionFrame => CurrentFrame.IsActionFrame;
    public bool IsPreviousFrameActionFrame => PreviousFrame.IsActionFrame;
    public bool IsNextFrameActionFrame => NextFrame.IsActionFrame;
    public bool IsCurrentFrameFirstFrame => _isFirstFrame;
    public float CurrentTime => _currentAnimation.TimeBefore(_currentFrameIndex) + TimeSinceCurrentFrameStart;
    public float ProgressAfterFirstActionFrame => (CurrentTime - _currentAnimation.TimeUpToAndIncludingFirstActionFrame) / _currentAnimation.TimeAfterFirstActionFrame;
    public float ProgressOfFirstActionFrame => (CurrentTime - _currentAnimation.TimeBeforeFirstActionFrame) / _currentAnimation.DurationOfFirstActionFrame;

    [SerializeField]
    private SpriteBody _body;
    [SerializeField]
    private SpriteAnimation _currentAnimation;
    [SerializeField]
    private int _currentFrameIndex;
    private float _currentFrameStartTime;
    private float _currentAnimationStartTime;
    private SpriteAnimationTransition _activeTransition;
    private bool _isFirstFrame = true;

    private void Reset()
    {
        _body = GetComponent<SpriteBody>();
    }
    private void OnValidate()
    {
        if (_body == null || _currentAnimation == null)
        {
            return;
        }
        UpdateSpriteBody();
    }
    private void OnEnable()
    {
        UpdateSpriteBody();
    }
    private void Update()
    {
        if (ReadyToTransition())
        {
            ApplyTransition();
        }
        if (ReadyToAdvanceOneFrame())
        {
            AdvanceOneFrame();
        }
    }

    public void SetTransition(SpriteAnimationTransition transition)
    {
        _activeTransition = transition;
        if (Mathf.Approximately(transition.Scale, 0))
        {
            ApplyTransition();
        }
    }
    private void ApplyTransition()
    {
        _activeTransition.ApplyFlip(_body);
        SetAnimation(_activeTransition.Animation);
    }
    public void SetAnimation(SpriteAnimation animation)
    {
        _currentAnimation = animation;
        _currentFrameIndex = 0;
        _activeTransition = SpriteAnimationTransition.Empty;
        _isFirstFrame = true;
        _currentAnimationStartTime = Time.time;
        UpdateSpriteBody();
        StartedAnimation();
    }
    public bool IsAnimating(SpriteAnimation animation)
    {
        return _currentAnimation == animation;
    }
    public void SetFlipX(bool flipX)
    {
        _body.SetFlipX(flipX);
    }

    private bool ReadyToTransition()
    {
        return IsTransitioningOnCurrentFrame() && IsCurrentFrameFinished;
    }
    private bool ReadyToAdvanceOneFrame()
    {
        return !IsTransitioningOnCurrentFrame() && IsCurrentFrameFinished;
    }
    public bool IsTransitioningOnCurrentFrame()
    {
        return IsTransitioning() && CurrentFrame.IsTransitionFrame;
    }
    private bool IsTransitioning()
    {
        return !_activeTransition.IsEmpty;
    }

    private void AdvanceOneFrame()
    {
        bool isLastFrame = _currentAnimation.IsLastFrame(_currentFrameIndex);
        _currentFrameIndex++;
        _isFirstFrame = false;
        UpdateSpriteBody();
        if (isLastFrame)
        {
            FinishedAnimation();
        }
    }
    private void UpdateSpriteBody()
    {
        _body.ShowFrame(CurrentFrame);
        _currentFrameStartTime = Time.time;
        if (IsCurrentFrameActionFrame)
        {
            ActionFrameEntered();
        }
    }
}
