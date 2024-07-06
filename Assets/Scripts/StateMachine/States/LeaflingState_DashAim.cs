using UnityEngine;
using NaughtyAttributes;

namespace Leafling
{
    public class LeaflingState_DashAim : LeaflingState
    {
        [SerializeField]
        private float _gravityScale = 0.1f;
        [SerializeField]
        private float _animationTransitionScale = 0.25f;
        [SerializeField, ReadOnly]
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
            _aim = LeaflingStateTool_Dash.ClampDashAim(Leafling, Leafling.DashAim);
            LeaflingStateTool_Dash.TransitionDashPerch(Leafling, _animationTransitionScale, _aim);
            if (!Leafling.IsAimingDash)
            {
                Leafling.SendSignal(new LeaflingSignal_Dash(_aim, true));
            }
        }
    }
}