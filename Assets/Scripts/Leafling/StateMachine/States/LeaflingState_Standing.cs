using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Standing : LeaflingState
    {
        private FromToCurve _toRun;
        private FromToCurve _toLeap;

        protected override void Awake()
        {
            base.Awake();
            _toRun = new FromToCurve(0, Leafling.BaseRunSpeed, Leafling.RunAccelerationCurve);
            _toLeap = new FromToCurve(Leafling.BaseRunSpeed, Leafling.LeapMaxSpeed, Leafling.RunAccelerationCurve);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            TransitionAnimation(0);
        }

        protected override void OnHorizontalDirectionChanged()
        {
            base.OnHorizontalDirectionChanged();
            TransitionAnimation(Leafling.RunningTransitionScale);
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
            if (Leafling.IsAnimating(Leafling.Idle))
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
                return Leafling.FacingDirection * Leafling.LeapMaxSpeed;
            }
            return Leafling.FacingDirection * Leafling.BaseRunSpeed;
        }

        private void TransitionAnimation(float scale)
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.SetTransition(new(Leafling.Idle, scale, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(Leafling.Run, scale, FlipXFromHorizontalDirection()));
            }
        }
        private bool FlipXFromHorizontalDirection()
        {
            return Leafling.DirectionToFlipX(Leafling.HorizontalDirection);
        }
    }
}