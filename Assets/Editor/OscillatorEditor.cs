#if UNITY_EDITOR
using _Scripts;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(_Scripts.Oscillator))]
    public class OscillatorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            
            var oscillator = (Oscillator) target;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("oscillate"));
            var useCurveProp = serializedObject.FindProperty("useCurve");
            EditorGUILayout.PropertyField(useCurveProp);
            if (!useCurveProp.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("amplitude"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("frequency"));
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("oscillationCurve"));
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("amplitude"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("frequency"));
                GUI.enabled = true;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("oscillationCurve"));
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif