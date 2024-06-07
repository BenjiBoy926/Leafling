namespace Leafling
{
    public class LeaflingSlideState : LeaflingState
    {
        public LeaflingSlideState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Slide, Leafling.SlideTransitionScale, Leafling.CurrentFlipX));
        }

        protected override void OnStartedJumping()
        {
            base.OnStartedJumping();
            if (HasEnteredActionFrame)
            {
                Leafling.SetState(new LeaflingLongJumpState(Leafling));
            }
        }
        protected override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            Leafling.SetHorizontalVelocity(0);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Slide))
            {
                Leafling.SetState(new LeaflingStandingState(Leafling));
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (HasEnteredActionFrame)
            {
                float t = Leafling.ProgressOfFirstActionFrame;
                float speed = Leafling.EvaluateSlideSpeedCurve(t) * Leafling.MaxSlideSpeed;
                Leafling.SetHorizontalVelocity(speed * Leafling.FacingDirection);
            }
        }
    }
}