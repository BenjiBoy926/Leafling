using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public struct Cooldown
    {
        public bool IsCooledDown => TimeSinceStart >= _duration;
        private float TimeSinceStart => Time.time - _startTime;

        private float _duration;
        private float _startTime;

        public Cooldown(float duration)
        {
            _duration = duration;
            _startTime = 0;
        }

        public void Start()
        {
            _startTime = Time.time;
        }
    }
}
