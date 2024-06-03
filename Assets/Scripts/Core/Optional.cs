using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct Optional<TValue>
    {
        public bool HasValue => _hasValue;
        public TValue Value => _value;

        [SerializeField]
        private bool _hasValue;
        [SerializeField]
        private TValue _value;

        public Optional(TValue value)
        {
            _hasValue = true;
            _value = value;
        }
    }
}