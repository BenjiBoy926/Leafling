using UnityEngine;
using NaughtyAttributes;

namespace Leafling
{
    public class LeaflingState_Dash : LeaflingState
    {
        [SerializeField]
        private SpriteAnimation _animation;
        [SerializeField]
        private float _maxSpeed;
        [SerializeField]
        private AnimationCurve _speedCurve;

        [SerializeField, ReadOnly]
        private Vector2 _aim;
        [SerializeField, ReadOnly]
        private bool _dashOnRicochet;

        public void SetAim(Vector2 aim)
        {
            _aim = aim;
        }
        public void SetDashOnRicochet(bool dashOnRicochet)
        {
            _dashOnRicochet = dashOnRicochet;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Leafling.SetAnimation(_animation);
            Leafling.FaceTowards(_aim.x);
            LeaflingStateTool_Dash.SetMidairRotation(Leafling, _aim);
            Leafling.MakeUnableToDash();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Leafling.ResetSpriteRotation();
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(_animation))
            {
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
        }

        protected override void Update()
        {
            base.Update();
            Vector2 velocity = GetDashVelocity();
            Leafling.SetVelocity(velocity);
            CheckForRicochet();
        }
        private Vector2 GetDashVelocity()
        {
            if (Leafling.IsCurrentFrameActionFrame)
            {
                return _maxSpeed * _aim;
            }
            return _maxSpeed * _speedCurve.Evaluate(Leafling.ProgressAfterFirstActionFrame) * _aim;
        }

        private void CheckForRicochet()
        {
            if (!CanRicochet())
            {
                return;
            }
            if (GetRicochetNormal(out Vector2 normal))
            {
                Ricochet(normal);
            }
        }
        private bool CanRicochet()
        {
            return Leafling.IsTouchingAnything();
        }
        private bool GetRicochetNormal(out Vector2 normal)
        {
            normal = Vector2.zero;
            foreach (Vector2 contactNormal in Leafling.GetContactNormals())
            {
                if (CanRicochetOffOfNormal(contactNormal))
                {
                    normal = contactNormal;
                    return true;
                }
            }
            return false;
        }
        private bool CanRicochetOffOfNormal(Vector2 normal)
        {
            return Vector2.Dot(_aim, normal) < -0.01f;
        }
        private void Ricochet(Vector2 normal)
        {
            Vector2 ricochetDirection = GetRicochetAim(normal);
            if (_dashOnRicochet)
            {
                Leafling.SendSignal(new LeaflingSignal_DashSquat(ricochetDirection, false));
            }
            else
            {
                Leafling.SendSignal(new LeaflingSignal_DashCancel(ricochetDirection));
            }
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}