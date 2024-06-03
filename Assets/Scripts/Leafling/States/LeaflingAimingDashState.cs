using UnityEngine;
using UnityEngine.UIElements;

namespace Leafling
{
    public class LeaflingAimingDashState : LeaflingState
    {
        private Vector2 _aim;

        public LeaflingAimingDashState(Leafling leafling) : base(leafling) 
        { 
            _aim = leafling.DashAim;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetVerticalVelocity(0);
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetSpriteRotation();
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            _aim = CalculateDashAim();
            TransitionToAnimation();
            SetSpriteRotation();
            if (!Leafling.IsAimingDash)
            {
                Leafling.SetState(new LeaflingDashState(Leafling, _aim, true));
            }
        }

        private Vector2 CalculateDashAim()
        {
            Vector2 aim = Leafling.DashAim;
            foreach (Vector2 normal in Leafling.GetContactNormals())
            {
                aim = ClampAbovePlane(aim, normal);
            }
            return aim;
        }
        private Vector2 ClampAbovePlane(Vector2 vector, Vector2 normal)
        {
            if (Vector2.Dot(vector, normal) < 0)
            {
                return Vector3.ProjectOnPlane(vector, normal).normalized;
            }
            return vector;
        }
        private void TransitionToAnimation()
        {
            if (Leafling.IsTouching(CardinalDirection.Down))
            {
                Leafling.SetTransition(new(Leafling.Squat, 0.5f, FlipXTowardsAim()));
            }
            else if (Leafling.IsTouching(CardinalDirection.Up))
            {
                Leafling.SetTransition(new(Leafling.CeilingPerch, 0.5f, FlipXTowardsAim()));
            }
            else if (Leafling.IsTouching(CardinalDirection.Right))
            {
                Leafling.SetTransition(new(Leafling.WallPerch, 0.5f, Leafling.DirectionToFlipX(-1)));
            }
            else if (Leafling.IsTouching(CardinalDirection.Left))
            {
                Leafling.SetTransition(new(Leafling.WallPerch, 0.5f, Leafling.DirectionToFlipX(1)));
            }
            else
            {
                Leafling.SetTransition(new(Leafling.MidairDashAim, 0.5f, FlipXTowardsAim()));
            }
        }
        private bool FlipXTowardsAim()
        {
            return Leafling.DirectionToFlipX(Leafling.DashAimX);
        }
        private void SetSpriteRotation()
        {
            if (Leafling.IsTouchingAnything())
            {
                Leafling.ResetSpriteRotation();
            }
            else
            {
                LeaflingDashTools.SetRotation(Leafling, _aim);
            }
        }
    }
}