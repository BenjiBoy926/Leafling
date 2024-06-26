using UnityEngine;
using UnityEngine.UIElements;

namespace Leafling
{
    public class LeaflingState_DashAim : LeaflingState
    {
        [SerializeField]
        private float _gravityScale = 0.1f;
        [SerializeField]
        private float _animationTransitionScale = 0.25f;
        private Vector2 _aim;

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetGravityScale(_gravityScale);
            Leafling.SetVerticalVelocity(0);
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.ResetGravityScale();
            Leafling.ResetSpriteRotation();
        }
        protected override void Update()
        {
            base.Update();
            _aim = CalculateDashAim();
            LeaflingStateTool_Dash.TransitionDashPerch(Leafling, _animationTransitionScale, _aim);
            if (!Leafling.IsAimingDash)
            {
                Leafling.SendSignal(new LeaflingSignal_Dash(_aim, true));
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