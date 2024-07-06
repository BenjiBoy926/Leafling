using System;
using UnityEngine;

public interface IStateMachine
{
    string StateListPropertyPath { get; }
    Type BaseStateType { get; }
    MonoBehaviour Behaviour { get; }
}
