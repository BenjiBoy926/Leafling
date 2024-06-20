using UnityEditor;
using UnityEngine;

namespace Leafling
{
    [CustomPropertyDrawer(typeof(ColorPair))]
    public class ColorPairDrawer : PropertyDrawer
    {
        private const float KeyLabelWidth = 30;
        private const float ValueLabelWidth = 40;
        private const float Space = 20;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float colorWidth = (position.width - ValueLabelWidth - KeyLabelWidth - Space) / 2;
            SerializedProperty key = property.FindPropertyRelative(ColorPair.KeyBackingFieldName);
            SerializedProperty value = property.FindPropertyRelative(ColorPair.ValueBackingFieldName);

            position.width = KeyLabelWidth;
            EditorGUI.LabelField(position, key.displayName);

            position.x += position.width;
            position.width = colorWidth;
            EditorGUI.PropertyField(position, key, GUIContent.none);

            position.x += position.width + Space;
            position.width = ValueLabelWidth;
            EditorGUI.LabelField(position, value.displayName);

            position.x += position.width;
            position.width = colorWidth;
            EditorGUI.PropertyField(position, value, GUIContent.none);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    } 
}