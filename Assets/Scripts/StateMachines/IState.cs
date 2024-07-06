using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    MonoBehaviour Behaviour { get; }
    void RefreshTarget();
}
