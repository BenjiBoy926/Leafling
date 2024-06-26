using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Jump : LeaflingState
    {
        private float JumpProgress => TimeSinceStateStart / Leafling.MaxJumpTime;

        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _maxSpeed;
        [SerializeField]
        private float _maxDuration;
        [SerializeField]
        private AnimationCurve _speedCurve;
        [SerializeField]
        private DirectionalAirControl _airControl;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetAnimation(_animation);
        }

        protected override void Update()
        {
            base.Update();
            Leafling.ApplyAirControl(_airControl);
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
            return TimeSinceStateStart >= _maxDuration;
        }
        private float GetJumpSpeed()
        {
            return _speedCurve.Evaluate(JumpProgress) * _maxSpeed;
        }
    }
}