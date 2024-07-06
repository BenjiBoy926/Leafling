using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Standing : LeaflingState
    {
        private float MaxSpeed => _baseSpeed + _leapAdditionalSpeed;

        [SerializeField]
        private SpriteAnimation _idle;
        [SerializeField] 
        private SpriteAnimation _running;
        [SerializeField]
        private float _animationTransitionScale = 0.25f;
        [SerializeField]
        private float _baseSpeed = 5;
        [SerializeField]
        private float _leapAdditionalSpeed = 15;
        [SerializeField]
        private AnimationCurve _accelerationCurve;
        private FromToCurve _toRun;
        private FromToCurve _toLeap;

        private void Awake()
        {
            _toRun = new FromToCurve(0, _baseSpeed, _accelerationCurve);
            _toLeap = new FromToCurve(_baseSpeed, MaxSpeed, _accelerationCurve);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            TransitionAnimation(0);
        }

        protected override void OnHorizontalDirectionChanged()
        {
            base.OnHorizontalDirectionChanged();
            TransitionAnimation(_animationTransitionScale);
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Target.SendSignal(new LeaflingSignal<LeaflingState_JumpSquat>());
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Target.IsAbleToDash)
            {
                Target.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
            }
        }
        protected override void OnStartedCrouching()
        {
            base.OnStartedCrouching();
            if (Target.HorizontalDirection != 0)
            {
                Target.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
            }
        }

        protected override void Update()
        {
            base.Update();
            float speed = CalculateHorizontalSpeed();
            Target.SetHorizontalVelocity(speed);
            if (!Target.IsTouching(CardinalDirection.Down))
            {
                Target.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
            if (Target.IsCrouching && Target.HorizontalDirection == 0)
            {
                Target.SendSignal(new LeaflingSignal<LeaflingState_Crouch>());
            }
        }
        private float CalculateHorizontalSpeed()
        {
            if (Target.IsAnimating(_idle))
            {
                return 0;
            }
            if (Target.IsCurrentFrameFirstFrame)
            {
                return Target.FacingDirection * _toRun.Evaluate(Target.CurrentFrameProgress);
            }
            if (Target.IsTransitioningOnCurrentFrame)
            {
                return Target.FacingDirection * _toRun.Evaluate(1 - Target.CurrentFrameProgress);
            }
            if (Target.IsNextFrameActionFrame)
            {
                return Target.FacingDirection * _toLeap.Evaluate(Target.CurrentFrameProgress);
            }
            if (Target.IsPreviousFrameActionFrame)
            {
                return Target.FacingDirection * _toLeap.Evaluate(1 - Target.CurrentFrameProgress);
            }
            if (Target.IsCurrentFrameActionFrame)
            {
                return Target.FacingDirection * MaxSpeed;
            }
            return Target.FacingDirection * _baseSpeed;
        }

        private void TransitionAnimation(float scale)
        {
            if (Target.HorizontalDirection == 0)
            {
                Target.SetTransition(new(_idle, scale, Target.CurrentFlipX));
            }
            else
            {
                Target.SetTransition(new(_running, scale, FlipXFromHorizontalDirection()));
            }
        }
        private bool FlipXFromHorizontalDirection()
        {
            return Leafling.DirectionToFlipX(Target.HorizontalDirection);
        }
    }
}