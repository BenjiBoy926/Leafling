using UnityEngine;
using NaughtyAttributes;
using System.Collections;

public class LeaflingState_TargetStrike : LeaflingState
{
    [SerializeField]
    private float _duration = 0.2f;
    [SerializeField]
    private float _magnitude = 0.2f;
    [SerializeField]
    private int _shakeCount = 3;
    [SerializeField, ReadOnly]
    private DashTarget _dashSpin;

    public static void Enter(Leafling leafling, DashTarget target)
    {
        LeaflingState_TargetStrike state = leafling.GetState<LeaflingState_TargetStrike>();
        state._dashSpin = target;
        leafling.SetState(state);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SpriteAnimator animator = Target.Animator;
        Transform spriteTransform = animator.transform;
        StopAllCoroutines();
        StartCoroutine(Shake(spriteTransform));
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopAllCoroutines();
        Target.ResetSpriteRotation();
    }

    protected override void Update()
    {
        base.Update();
        Target.SetVelocity(Vector2.zero);
    }

    private IEnumerator Shake(Transform transform)
    {
        float singleOffsetDuration = _duration / 2 / _shakeCount;
        WaitForSeconds singleOffsetWait = new(singleOffsetDuration);
        for (int i = 0; i < _shakeCount; i++)
        {
            float damp = (_shakeCount - i) / (float)_shakeCount;
            float xPosition = _magnitude * damp;

            transform.SetLocalX(xPosition);
            yield return singleOffsetWait;

            transform.SetLocalX(-xPosition);
            yield return singleOffsetWait;
        }

        transform.SetLocalX(0);
        LeaflingState_DashSpin.Enter(Target, _dashSpin);
    }
}
