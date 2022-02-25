using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InspectorReadOnlyAttribute))]
public class InspectorReadOnlyPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
