using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public class HueMap
    {
        public int Count => _keyValuePairs.Length;
        public List<Vector4> Keys => _keyValuePairs.Select(Key).ToList();
        public List<Vector4> Values => _keyValuePairs.Select(Value).ToList();

        [SerializeField]
        private ColorPair[] _keyValuePairs;

        private Vector4 Key(ColorPair pair)
        {
            return pair.Key;
        }
        private Vector4 Value(ColorPair pair)
        {
            return pair.Value;
        }
    }
}