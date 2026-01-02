using UnityEngine;
using NaughtyAttributes;

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
    [SerializeField, ReadOnly]
    private DashTarget _struckTarget;

    public static void Enter(Leafling target, Vector2 aim, bool dashOnRicochet)
    {
        LeaflingState_Dash state = target.GetState<LeaflingState_Dash>();
        state._aim = aim;
        state._dashOnRicochet = dashOnRicochet;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _struckTarget = null;
        Target.SetAnimation(_animation);
        Target.FaceTowards(_aim.x);
        LeaflingStateTool_Dash.SetMidairRotation(Target, _aim);
        
        Target.MakeUnableToDash();

        Target.TakeControlOfReticle();
        Target.ShowAim(_aim);
        Target.FlashDashReticle();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if (!_struckTarget)
        {
            Target.ResetSpriteRotation();
        }
        Target.ReleaseControlOfReticle();
        Target.ClearDashReticleHighlight();
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        if (Target.IsAnimating(_animation))
        {
            LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
        }
    }
    protected override void OnDashTargetTouched(DashTarget target)
    {
        base.OnDashTargetTouched(target);
        _struckTarget = target;
        LeaflingState_TargetStrike.Enter(Target, target);
    }

    protected override void Update()
    {
        base.Update();
        Vector2 velocity = GetDashVelocity();
        Target.SetVelocity(velocity);
        CheckForRicochet();
    }
    private Vector2 GetDashVelocity()
    {
        if (Target.IsCurrentFrameActionFrame)
        {
            return _maxSpeed * _aim;
        }
        return _maxSpeed * _speedCurve.Evaluate(Target.ProgressAfterFirstActionFrame) * _aim;
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
        return Target.IsTouchingAnything();
    }
    private bool GetRicochetNormal(out Vector2 normal)
    {
        normal = Vector2.zero;
        foreach (Vector2 contactNormal in Target.GetContactNormals())
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
            LeaflingState_DashSquat.Enter(Target, ricochetDirection, false);
        }
        else
        {
            LeaflingState_DashCancel.Enter(Target, ricochetDirection);
        }
    }
    private Vector2 GetRicochetAim(Vector2 normal)
    {
        return Vector2.Reflect(_aim, normal);
    }
}
