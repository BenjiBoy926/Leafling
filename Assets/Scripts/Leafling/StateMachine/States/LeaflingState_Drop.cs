namespace Leafling
{
    public class LeaflingState_Drop : LeaflingState
    {
        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Drop, Leafling.DropTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(Leafling.DropCancelSpeed);
            }
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }

        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
            Leafling.ApplyAirControl(Leafling.DropAirControl);
            if (HasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(-Leafling.DropSpeed);
            }
            else
            {
                Leafling.SetVerticalVelocity(0);
            }
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SendSignal(new LeaflingSignal_Landing(new LeaflingSignal<LeaflingState_DropJump>()));
            }
        }
    }
}