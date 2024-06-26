using UnityEngine;

namespace Leafling
{
    public class LeaflingState_Dash : LeaflingState
    {
        private Vector2 _aim;
        private bool _dashOnRicochet;

        public void SetAim(Vector2 aim)
        {
            _aim = aim;
        }
        public void SetDashOnRicochet(bool dashOnRicochet)
        {
            _dashOnRicochet = dashOnRicochet;
        }

        public override void Enter()
        {
            base.Enter();
            Leafling.SetAnimation(Leafling.Dash);
            Leafling.FaceTowards(_aim.x);
            LeaflingStateTool_Dash.SetMidairRotation(Leafling, _aim);
            Leafling.MakeUnableToDash();
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
                Leafling.SendSignal(new LeaflingSignal_FreeFall(FreeFallEntry.Backflip));
            }
        }

        public override void Update_Obsolete(float dt)
        {
            base.Update_Obsolete(dt);
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