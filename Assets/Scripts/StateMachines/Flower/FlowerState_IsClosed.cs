using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerState_IsClosed : FlowerState
{
    protected override void OnFlowerStruck()
    {
        base.OnFlowerStruck();
        Target.SetState<FlowerState_IsOpen>();
    }
}
