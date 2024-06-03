using UnityEngine;

namespace Leafling
{
    public class LeaflingJumpState : LeaflingState
    {
        private float TimeSinceJumpActionFrameStart => Time.time - _timeOfJumpActionFrameStart;
        private float JumpProgress => TimeSinceJumpActionFrameStart / Leafling.MaxJumpTime;

        private float _timeOfJumpActionFrameStart = -1;

        public LeaflingJumpState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Jump, 0.1f, Leafling.CurrentFlipX));
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Leafling.IsAnimatingJump)
            {
                return;
            }
            if (StartedJumpActionThisFrame())
            {
                _timeOfJumpActionFrameStart = Time.time;
            }
            if (Leafling.IsCurrentFrameActionFrame)
            {
                Leafling.SetVerticalVelocity(GetJumpSpeed());
            }
            if (ShouldTransitionOutOfJump())
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Backflip));
            }
            Leafling.ApplyJumpAirControl();
        }
        private bool ShouldTransitionOutOfJump()
        {
            return IsLeaflingAnimatingJumpActionFrame() && ((!Leafling.IsJumping && TimeSinceJumpActionFrameStart >= Leafling.MinJumpTime) || (TimeSinceJumpActionFrameStart >= Leafling.MaxJumpTime));
        }
        private float GetJumpSpeed()
        {
            return Leafling.EvaluateJumpSpeedCurve(JumpProgress) * Leafling.MaxJumpSpeed;
        }
        private bool StartedJumpActionThisFrame()
        {
            return IsLeaflingAnimatingJumpActionFrame() && !IsTimeOfJumpActionStartSet();
        }
        private bool IsLeaflingAnimatingJumpActionFrame()
        {
            return Leafling.IsAnimatingJump && Leafling.IsCurrentFrameActionFrame;
        }
        private bool IsTimeOfJumpActionStartSet()
        {
            return _timeOfJumpActionFrameStart >= 0;
        }
    }
}