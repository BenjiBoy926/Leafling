using System;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public class ColorPair
    {
        [field: SerializeField]
        public Color Key { get; private set; } = Color.white;
        [field: SerializeField]
        public Color Value { get; private set; } = Color.white;
    }
}