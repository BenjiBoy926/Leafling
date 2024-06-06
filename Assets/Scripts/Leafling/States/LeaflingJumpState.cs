using UnityEngine;

namespace Leafling
{
    public class LeaflingJumpState : LeaflingState
    {
        private float JumpProgress => TimeSinceStateStart / Leafling.MaxJumpTime;

        public LeaflingJumpState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Jump);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Leafling.ApplyAirControl(Leafling.JumpAirControl);
            Leafling.SetVerticalVelocity(GetJumpSpeed());
            if (ShouldTransitionOutOfJump())
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
            }
        }
        private bool ShouldTransitionOutOfJump()
        {
            return IsAirborn() && (IsJumpingInputReleased() || IsJumpingTimeExhausted());
        }
        private bool IsAirborn()
        {
            return !Leafling.IsTouching(CardinalDirection.Down);
        }
        private bool IsJumpingInputReleased()
        {
            return !Leafling.IsJumping;
        }
        private bool IsJumpingTimeExhausted()
        {
            return TimeSinceStateStart >= Leafling.MaxJumpTime;
        }
        private float GetJumpSpeed()
        {
            return Leafling.EvaluateJumpSpeedCurve(JumpProgress) * Leafling.MaxJumpSpeed;
        }
    }
}