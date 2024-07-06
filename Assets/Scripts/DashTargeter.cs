using System;
using UnityEngine;

public class DashTargeter : MonoBehaviour
{
    public delegate void TargetStrikeHandler(DashTarget target);
    public event TargetStrikeHandler TouchedTarget = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DashTarget target))
        {
            TouchedTarget(target);
        }
    }
}