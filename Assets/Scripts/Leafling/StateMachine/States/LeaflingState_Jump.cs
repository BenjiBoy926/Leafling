using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Jump : LeaflingState
    {
        private float JumpProgress => TimeSinceStateStart / Leafling.MaxJumpTime;

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Jump);
        }

        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
            Leafling.ApplyAirControl(Leafling.JumpAirControl);
            Leafling.SetVerticalVelocity(GetJumpSpeed());
            if (ShouldTransitionOutOfJump())
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
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