using UnityEngine;

namespace Leafling
{
    public class LeaflingOnGroundState : LeaflingState
    {
        private readonly FromToCurve _toRun;
        private readonly FromToCurve _toLeap;

        public LeaflingOnGroundState(Leafling leafling) : base(leafling) 
        {
            _toRun = new FromToCurve(0, leafling.BaseRunSpeed, leafling.RunAccelerationCurve);
            _toLeap = new FromToCurve(leafling.BaseRunSpeed, leafling.LeapMaxSpeed, leafling.RunAccelerationCurve);
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.HorizontalDirectionChanged += OnLeaflingDirectionChanged;
            ReflectCurrentDirection();
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.HorizontalDirectionChanged -= OnLeaflingDirectionChanged;
        }

        private void OnLeaflingDirectionChanged()
        {
            ReflectCurrentDirection();
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
            if (Leafling.IsJumping)
            {
                Leafling.SetState(new LeaflingJumpState(Leafling));
            }
            if (Leafling.IsAimingDash)
            {
                Leafling.SetState(new LeaflingAimingDashState(Leafling));
            }
        }
        private float CalculateHorizontalSpeed()
        {
            if (Leafling.IsAnimatingIdle)
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
                Leafling.SetTransition(new(Leafling.Idle, 0.3f, Leafling.CurrentFlipX));
            }
            else
            {
                Leafling.SetTransition(new(Leafling.Run, 0.3f, IntendedFlipX()));
            }
        }
        private bool IntendedFlipX()
        {
            return Leafling.DirectionToFlipX(Leafling.HorizontalDirection);
        }
    }
}