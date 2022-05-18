using UnityEngine;

namespace Sensors
{
    public class VisionConeVisualizer : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        private void Start()
        {
            var mesh = new Mesh();
            var vertices = new Vector3[3];
            var uv = new Vector2[3];
            var triangles = new int[3];

            vertices[0] = Vector3.zero;
            vertices[1] = new Vector3(50, 0);
            vertices[2] = new Vector3(0, -50);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            meshFilter.mesh = mesh;
        }
    }
}