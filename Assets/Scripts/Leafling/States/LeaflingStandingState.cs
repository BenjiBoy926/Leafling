using UnityEngine;

namespace Leafling
{
    public class LeaflingStandingState : LeaflingState
    {
        private readonly FromToCurve _toRun;
        private readonly FromToCurve _toLeap;

        public LeaflingStandingState(Leafling leafling) : base(leafling) 
        {
            _toRun = new FromToCurve(0, leafling.BaseRunSpeed, leafling.RunAccelerationCurve);
            _toLeap = new FromToCurve(leafling.BaseRunSpeed, leafling.LeapMaxSpeed, leafling.RunAccelerationCurve);
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Squat);
            ReflectCurrentDirection();
        }

        protected override void OnHorizontalDirectionChanged()
        {
            base.OnHorizontalDirectionChanged();
            ReflectCurrentDirection();
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            Leafling.SetState(new LeaflingJumpState(Leafling));
        }
        protected override void OnStartedAimingDash()
        {
            base.OnStartedAimingDash();
            Leafling.SetState(new LeaflingAimingDashState(Leafling));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            float speed = CalculateHorizontalSpeed();
            Leafling.SetHorizontalVelocity(speed);
            if (!Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
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

        private void ReflectCurrentDirection()
        {
            TransitionAnimation();
        }

        private void TransitionAnimation()
        {
            if (Leafling.HorizontalDirection == 0)
            {
                Leafling.SetTransition(new(Leafling.Idle, Leafling.RunningTransitionScale, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(Leafling.Run, Leafling.RunningTransitionScale, FlipXFromHorizontalDirection()));
            }
        }
        private bool FlipXFromHorizontalDirection()
        {
            return Leafling.DirectionToFlipX(Leafling.HorizontalDirection);
        }
    }
}