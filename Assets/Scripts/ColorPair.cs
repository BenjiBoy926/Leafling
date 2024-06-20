using System;
using UnityEngine;

namespace Leafling
{
    [Serializable]
    public class ColorPair
    {
        private const string BackingFieldFormat = "<{0}>k__BackingField";
        public static string KeyBackingFieldName => string.Format(BackingFieldFormat, nameof(Key));
        public static string ValueBackingFieldName => string.Format(BackingFieldFormat, nameof(Value));

        [field: SerializeField]
        public Color Key { get; private set; } = Color.white;
        [field: SerializeField]
        public Color Value { get; set; } = Color.white;
    }
}