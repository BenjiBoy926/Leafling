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

        protected override void Awake()
        {
            base.Awake();
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
            Leafling.SendSignal(new LeaflingSignal<LeaflingState_JumpSquat>());
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            if (Leafling.IsAbleToDash)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_DashAim>());
            }
        }
        protected override void OnStartedCrouching()
        {
            base.OnStartedCrouching();
            if (Leafling.HorizontalDirection != 0)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Slide>());
            }
        }

        protected override void Update()
        {
            base.Update();
            float speed = CalculateHorizontalSpeed();
            Leafling.SetHorizontalVelocity(speed);
            if (!Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
            if (Leafling.IsCrouching && Leafling.HorizontalDirection == 0)
            {
                Leafling.SendSignal(new LeaflingSignal<LeaflingState_Crouch>());
            }
        }
        private float CalculateHorizontalSpeed()
        {
            if (Leafling.IsAnimating(_idle))
            {
                return 0;
            }
            if (Leafling.IsCurrentFrameFirstFrame)
            {
                return Leafling.FacingDirection * _toRun.Evaluate(Leafling.CurrentFrameProgress);
            }
            if (Leafling.IsTransitioningOnCurrentFrame)
            {
                return Leafling.FacingDirection * _toRun.Evaluate(1 - Leafling.CurrentFrameProgress);
            }
            if (Leafling.IsNextFrameActionFrame)
            {
                return Leafling.FacingDirection * _toLeap.Evaluate(Leafling.CurrentFrameProgress);
            }
            if (Leafling.IsPreviousFrameActionFrame)
            {
                return Leafling.FacingDirection * _toLeap.Evaluate(1 - Leafling.CurrentFrameProgress);
            }
            if (Leafling.IsCurrentFrameActionFrame)
            {
                return Leafling.FacingDirection * MaxSpeed;
            }
            return Leafling.FacingDirection * _baseSpeed;
        }

        private void TransitionAnimation(float scale)
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.SetTransition(new(_idle, scale, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(_running, scale, FlipXFromHorizontalDirection()));
            }
        }
        private bool FlipXFromHorizontalDirection()
        {
            return Leafling.DirectionToFlipX(Leafling.HorizontalDirection);
        }
    }
}