using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Dash : LeaflingState
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public LeaflingState_Dash(Leafling leafling, Vector2 aim, bool dashOnRicochet) : base(leafling) 
        {
            _aim = aim;
            _dashOnRicochet = dashOnRicochet;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Dash);
            Leafling.FaceTowards(_aim.x);
            LeaflingDashTools.SetMidairRotation(Leafling, _aim);
        }
        public override void Exit()
        {
            base.Exit();
            Leafling.ResetSpriteRotation();
        }
        protected override void OnAnimationFinished()
        {
            base.OnAnimationFinished();
            if (Leafling.IsAnimating(Leafling.Dash))
            {
                Leafling.SetState(new LeaflingState_FreeFall(Leafling, FreeFallEntry.Backflip));
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Vector2 velocity = GetDashVelocity();
            Leafling.SetVelocity(velocity);
            CheckForRicochet();
        }
        private Vector2 GetDashVelocity()
        {
            if (Leafling.IsCurrentFrameActionFrame)
            {
                return Leafling.MaxDashSpeed * _aim;
            }
            return Leafling.MaxDashSpeed * Leafling.EvaluateDashSpeedCurve(Leafling.ProgressAfterFirstActionFrame) * _aim;
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
                Leafling.SetState(new LeaflingState_DashSquat(Leafling, ricochetDirection, false));
            }
            else
            {
                Leafling.SetState(new LeaflingState_DashCancel(Leafling, ricochetDirection));
            }
        }
        private Vector2 GetRicochetAim(Vector2 normal)
        {
            return Vector2.Reflect(_aim, normal);
        }
    }
}