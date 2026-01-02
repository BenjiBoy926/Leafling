using UnityEngine;
using NaughtyAttributes;

public class LeaflingState_DashCancel : LeaflingState
{
    [SerializeField]
    private float _speed;
    [SerializeField, ReadOnly]
    private Vector2 _direction;

    public static void Enter(Leafling target, Vector2 direction)
    {
        LeaflingState_DashCancel state = target.GetState<LeaflingState_DashCancel>();
        state._direction = direction;
        target.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LeaflingStateTool_Dash.ShowDashPerch(Target, _direction);
    }
    protected override void OnAnimationFinished()
    {
        base.OnAnimationFinished();
        _direction = LeaflingStateTool_Dash.ClampDashAim(Target, _direction);
        Target.SetVelocity(_direction.normalized * _speed);
        LeaflingState_FreeFall.Enter(Target, FreeFallEntry.Backflip);
    }
}
