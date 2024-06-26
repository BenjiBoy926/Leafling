using UnityEngine;

namespace Leafling
{
    public class LeaflingState_WallJump : LeaflingState
    {
        private CardinalDirection _wallDirection;

        public void SetWallDirection(CardinalDirection wallDirection)
        {
            _wallDirection = wallDirection;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetAnimation(Leafling.WallJump);
            Leafling.FaceTowards(LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection));
        }
        protected override void Update()
        {
            base.Update();
            float direction = LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection);
            Vector2 velocity = new Vector2(direction, 1).normalized * Leafling.WallJumpSpeed;
            Leafling.SetVelocity(velocity);
            if (ShouldFinishWallJump())
            {
                FinishWallJump();
            }
            if (Leafling.IsTouching(_wallDirection.Opposite))
            {
                Leafling.SendSignal(new LeaflingSignal_WallSlide(_wallDirection.Opposite, 0.5f));
            }
        }
        private bool ShouldFinishWallJump()
        {
            return !Leafling.IsTouching(_wallDirection) && (!Leafling.IsJumping || TimeSinceStateStart > Leafling.MaxWallJumpTime);
        }
        private void FinishWallJump()
        {
            Leafling.SetVerticalVelocity(Leafling.WallJumpExitHop);
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
}