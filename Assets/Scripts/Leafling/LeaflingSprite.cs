using NaughtyAttributes;
using UnityEngine;

namespace Leafling
{
    public class LeaflingSprite : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;
        [SerializeField, ReadOnly]
        private Color _leftArmDefaultColor;
        [SerializeField, ReadOnly]
        private Color _rightArmDefaultColor;

        private readonly int LeftArmColorID = Shader.PropertyToID("_LeftArmColor");
        private readonly int RightArmColorID = Shader.PropertyToID("_RightArmColor");
        private MaterialPropertyBlock _propertyBlock;

        private void Reset()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _leftArmDefaultColor = _renderer.sharedMaterial.GetColor(LeftArmColorID);
            _rightArmDefaultColor = _renderer.sharedMaterial.GetColor(RightArmColorID);
            ResetArmColor();
        }

        public void DesaturateArmColor()
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            DesaturateColor(LeftArmColorID);
            DesaturateColor(RightArmColorID);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
        public void ResetArmColor()
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            SetColor(LeftArmColorID, _leftArmDefaultColor);
            SetColor(RightArmColorID, _rightArmDefaultColor);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private void DesaturateColor(int id)
        {
            Color color = GetColor(id);
            Color.RGBToHSV(color, out float h, out float s, out float v);
            SetColor(id, Color.HSVToRGB(h, 0, v));
        }
        private Color GetColor(int id)
        {
            return _propertyBlock.GetColor(id);
        }
        private void SetColor(int id, Color color)
        {
            _propertyBlock.SetColor(id, color);
        }
    }
}