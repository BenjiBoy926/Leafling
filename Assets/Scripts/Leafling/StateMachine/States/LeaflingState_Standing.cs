using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Standing : LeaflingState
    {
        private readonly FromToCurve _toRun;
        private readonly FromToCurve _toLeap;

        public LeaflingState_Standing(Leafling leafling) : base(leafling) 
        {
            _toRun = new FromToCurve(0, leafling.BaseRunSpeed, leafling.RunAccelerationCurve);
            _toLeap = new FromToCurve(leafling.BaseRunSpeed, leafling.LeapMaxSpeed, leafling.RunAccelerationCurve);
        }

        public override void Enter()
        {
            base.Enter();
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

        public override void Update(float dt)
        {
            base.Update(dt);
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