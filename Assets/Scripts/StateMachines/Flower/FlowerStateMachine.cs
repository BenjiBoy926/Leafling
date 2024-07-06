using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerStateMachine : StateMachine<Flower>
{
    public override Type BaseStateType => typeof(FlowerState);
}
