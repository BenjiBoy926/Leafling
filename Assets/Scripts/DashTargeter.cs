using System;
using UnityEngine;

namespace Leafling
{
    public class DashTargeter : MonoBehaviour
    {
        public delegate void TargetStrikeHandler(DashTarget target);
        public event TargetStrikeHandler StruckTarget = delegate { };

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DashTarget target))
            {
                StruckTarget(target);
            }
        }
    }
}