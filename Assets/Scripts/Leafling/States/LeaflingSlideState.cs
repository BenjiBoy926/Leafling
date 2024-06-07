namespace Leafling
{
    public class LeaflingSlideState : LeaflingState
    {
        private bool _hasEnteredActionFrame;

        public LeaflingSlideState(Leafling leafling) : base(leafling)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new(Leafling.Slide, Leafling.SlideTransitionScale, Leafling.CurrentFlipX));
        }

        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Slide))
            {
                Leafling.SetState(new LeaflingStandingState(Leafling));
            }
        }
        protected override void OnAnimationEnteredActionFrame()
        {
            base.OnAnimationEnteredActionFrame();
            _hasEnteredActionFrame = true;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (_hasEnteredActionFrame)
            {
                float t = Leafling.ProgressOfFirstActionFrame;
                float speed = Leafling.EvaluateSlideSpeedCurve(t) * Leafling.MaxSlideSpeed;
                Leafling.SetHorizontalVelocity(speed * Leafling.FacingDirection);
            }
        }
    }
}