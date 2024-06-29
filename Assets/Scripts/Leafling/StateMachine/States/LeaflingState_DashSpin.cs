using System;
using System.Collections;
using UnityEngine;

namespace Leafling
{
    public class LeaflingState_DashSpin : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _exitHop = 30;
        private DashTarget _target;

        public void SetTarget(DashTarget target)
        {
            _target = target;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetPosition(_target.Position);
            Leafling.SetAnimation(_animation);
            Leafling.RestoreAbilityToDash();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.SetVerticalVelocity(_exitHop);
            Leafling.FaceTowards(-Leafling.FacingDirection);
        }
        protected override void Update()
        {
            base.Update();
            Leafling.SetVelocity(Vector2.zero);
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
        }
    }
}