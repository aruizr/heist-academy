using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editor
{
    [CustomEditor(typeof(VisionCone))]
    public class VisionConeEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var cone = (VisionCone) target;
            var transform = cone.transform;
            var position = transform.position;
            var up = transform.up;
            var forward = transform.forward;
            var fov = serializedObject.FindProperty("fieldOfView").floatValue;
            var distance = serializedObject.FindProperty("distance").floatValue;
            var color = serializedObject.FindProperty("gizmosColor").colorValue;
            var iterations = serializedObject.FindProperty("gizmosConeDetail").intValue;
            var deltaAngle = 180f / iterations;

            Handles.color = color;
            for (var a = 0f; a < 180f; a += deltaAngle) DrawArea(position, forward, up, fov, distance, a);
            
            Handles.color = Color.red;
            cone.VisibleObjects.ForEach(obj => Handles.DrawLine(position, obj.transform.position));
        }

        private void DrawArea(Vector3 position, Vector3 forward, Vector3 up, float fovAngle, float distance,
            float rotationAngles)
        {
            var normal = Quaternion.AngleAxis(rotationAngles, forward) * up;
            var maxEdge = Quaternion.AngleAxis(fovAngle * 0.5f, normal) * forward;
            var minEdge = Quaternion.AngleAxis(-fovAngle * 0.5f, normal) * forward;

            Handles.DrawWireArc(position, normal, minEdge, fovAngle, distance);
            Handles.DrawLine(position, position + maxEdge * distance);
            Handles.DrawLine(position, position + minEdge * distance);
        }
    }
}