namespace Leafling
{
    public class LeaflingState_JumpSquat : LeaflingState
    {
        public LeaflingState_JumpSquat(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Squat, Leafling.JumpTransitionScale, Leafling.CurrentFlipX));
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Squat))
            {
                Leafling.SendSignal(new LeaflingSignal_Generic<LeaflingState_Jump>());
            }
        }
    }
}