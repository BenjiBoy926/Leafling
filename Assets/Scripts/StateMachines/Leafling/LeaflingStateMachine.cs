using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingStateMachine : StateMachine<Leafling>
    {
        public override Type BaseStateType => typeof(LeaflingState);
    }
}