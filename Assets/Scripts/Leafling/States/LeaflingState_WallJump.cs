using UnityEngine;

namespace Leafling
{
    public class LeaflingState_WallJump : LeaflingState
    {
        private CardinalDirection _wallDirection;

        public LeaflingState_WallJump(Leafling leafling, CardinalDirection wallDirection) : base(leafling)
        {
            _wallDirection = wallDirection;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.WallJump);
            Leafling.FaceTowards(LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection));
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            float direction = LeaflingStateTool_WallJump.WallDirectionToFacingDirection(_wallDirection);
            Vector2 velocity = new Vector2(direction, 1).normalized * Leafling.WallJumpSpeed;
            Leafling.SetVelocity(velocity);
            if (ShouldFinishWallJump())
            {
                FinishWallJump();
            }
            if (Leafling.IsTouching(_wallDirection.Opposite))
            {
                Leafling.SetState(new LeaflingState_WallSlide(Leafling, _wallDirection.Opposite, 0.5f));
            }
        }
        private bool ShouldFinishWallJump()
        {
            return !Leafling.IsTouching(_wallDirection) && (!Leafling.IsJumping || TimeSinceStateStart > Leafling.MaxWallJumpTime);
        }
        private void FinishWallJump()
        {
            Leafling.SetVerticalVelocity(Leafling.WallJumpExitHop);
            Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
        }
    }
}