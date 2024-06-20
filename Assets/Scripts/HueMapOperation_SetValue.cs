using UnityEngine;

namespace Leafling
{
    public class HueMapOperation_SetValue
    {
        private int _index;
        private Color _value;

        public HueMapOperation_SetValue(int index, Color value)
        {
            _index = index;
            _value = value;
        }

        public void Perform(HueMap map)
        {
            map.SetValue(_index, _value);
        }
    }
}