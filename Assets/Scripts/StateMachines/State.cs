using System.Collections;
using UnityEngine;

public class State<TTarget> : MonoBehaviour, IState where TTarget : MonoBehaviour
{
    public MonoBehaviour Behaviour => this;
    [field: SerializeField]
    protected TTarget Target { get; private set; }
    protected float TimeSinceStateStart => Time.time - _timeOfStateStart;

    private float _timeOfStateStart;

    private void Reset()
    {
        RefreshTarget();
    }
    protected virtual void OnEnable()
    {
        _timeOfStateStart = Time.time;
    }
    public void RefreshTarget()
    {
        Target = GetComponentInParent<TTarget>();
    }
}