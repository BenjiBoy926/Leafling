using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leafling
{
    public class LeaflingState
    {
        protected Leafling Leafling => _leafling;
        protected float TimeSinceStateStart => Time.time - _timeOfStateStart;

        private Leafling _leafling;
        private float _timeOfStateStart;

        public LeaflingState(Leafling leafling)
        {
            _leafling = leafling;
        }
        public virtual void Enter()
        {
            _timeOfStateStart = Time.time;
        }
        public virtual void Update(float dt)
        {

        }
        public virtual void Exit()
        {

        }
    }
}