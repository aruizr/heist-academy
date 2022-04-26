using Interactions;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(SoundEmitter))]
    public class SoundEmitterEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var emitter = (SoundEmitter) target;
            var radius = emitter.Radius;
            var transform = emitter.transform;
            var color = serializedObject.FindProperty("gizmosColor").colorValue;

            Handles.color = color;
            Handles.DrawWireArc(transform.position, transform.up, transform.right, 360, radius);
        }
    }
}