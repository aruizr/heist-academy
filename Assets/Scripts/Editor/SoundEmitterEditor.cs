using Interactions;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SoundEmitter))]
    public class SoundEmitterEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var emitter = (SoundEmitter) target;
            var transform = emitter.transform;
            var radius = serializedObject.FindProperty("radius").floatValue;
            var color = serializedObject.FindProperty("gizmosColor").colorValue;

            Handles.color = color;
            Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, radius);
        }
    }
}