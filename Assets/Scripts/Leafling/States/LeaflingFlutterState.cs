namespace Leafling
{
    public class LeaflingFlutterState : LeaflingState
    {
        private bool _hasEnteredActionFrame = false;

        public LeaflingFlutterState(Leafling leafling) : base(leafling) { }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetTransition(new SpriteAnimationTransition(Leafling.Flutter, 0.5f, Leafling.CurrentFlipX));
            Leafling.AnimationFinished += OnAnimationFinished;
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.AnimationFinished -= OnAnimationFinished;
        }
        private void OnAnimationFinished()
        {
            if (Leafling.IsAnimating(Leafling.Flutter))
            {
                Leafling.SetState(new LeaflingFreeFallState(Leafling, FreeFallEntry.Normal));
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            HandleActionFrameEntry();
            ApplyAirControl();
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetState(new LeaflingOnGroundState(Leafling));
            }
        }
        private void HandleActionFrameEntry()
        {
            if (!Leafling.IsCurrentFrameActionFrame)
            {
                return;
            }
            if (!_hasEnteredActionFrame)
            {
                Leafling.SetVerticalVelocity(5);
            }
            _hasEnteredActionFrame = true;
        }
        private void ApplyAirControl()
        {
            if (Leafling.IsCurrentFrameActionFrame)
            {
                Leafling.ApplyFlutterAirControl();
            }
            else
            {
                Leafling.ApplyFreeFallAirControl();
            }
        }
    }
}