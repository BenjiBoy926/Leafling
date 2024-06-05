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
            Leafling.SetGravityScale(Leafling.AimingDashGravityScale);
            Leafling.SetVerticalVelocity(0);
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetGravityScale();
            Leafling.ResetSpriteRotation();
        }
        public override void Update(float dt)
        {
            base.Update(dt);
            _aim = CalculateDashAim();
            LeaflingDashTools.TransitionDashPerch(Leafling, Leafling.AimingDashTransitionScale, _aim);
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
    }
}