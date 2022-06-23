using Codetox.Messaging;
using UnityEngine;
using Variables;

namespace Sensors
{
    [ExecuteAlways]
    public class VisionConeVisualizer : MonoBehaviour
    {
        private static readonly int ViewSpaceMatrix = Shader.PropertyToID("_ViewSpaceMatrix");
        private static readonly int ViewDepthTexture = Shader.PropertyToID("_ViewDepthTexture");
        private static readonly int FieldOfView = Shader.PropertyToID("_FieldOfView");
        private static readonly int Color = Shader.PropertyToID("_Color");
        private static readonly int InnerRadius = Shader.PropertyToID("_InnerRadius");
        private static readonly int RadiusSaturation = Shader.PropertyToID("_RadiusSaturation");
        private static readonly int FieldOfViewSaturation = Shader.PropertyToID("_FieldOfViewSaturation");
        
        [SerializeField] private VisionCone visionCone;
        [SerializeField] private GameObject shaderObject;
        [SerializeField] private Material shaderMaterial;
        [SerializeField] private Camera perspectiveCamera;
        [SerializeField] private ValueReference<Color> color;
        [SerializeField] [Range(0, 1)] private float innerRadius;
        [SerializeField] [Min(0)] private float radiusSaturation = 10;
        [SerializeField] [Min(0)] private float fieldOfViewSaturation = 10;

        private Material _tempMaterial;

        private void Update()
        {
            if (!shaderObject || !perspectiveCamera || !visionCone || !shaderMaterial) return;

            var t = shaderObject.transform;

            t.localScale = visionCone.Distance * 2 * Vector3.one;
            t.localPosition = new Vector3(0, -visionCone.Distance, 0);
            t.localEulerAngles = new Vector3(0, -90, 0);

            perspectiveCamera.farClipPlane = visionCone.Distance;
            perspectiveCamera.fieldOfView = visionCone.FieldOfView;

            if (!perspectiveCamera.targetTexture ||
                perspectiveCamera.targetTexture.width != perspectiveCamera.pixelWidth ||
                perspectiveCamera.targetTexture.height != perspectiveCamera.pixelHeight)
                perspectiveCamera.targetTexture = new RenderTexture(
                    perspectiveCamera.pixelWidth,
                    perspectiveCamera.pixelHeight,
                    24,
                    RenderTextureFormat.Depth);

            perspectiveCamera.Render();

            var matrix = perspectiveCamera.projectionMatrix * perspectiveCamera.worldToCameraMatrix;

            if (!_tempMaterial)
            {
                _tempMaterial = new Material(shaderMaterial);
                shaderObject.Send<MeshRenderer>(meshRenderer => meshRenderer.sharedMaterial = _tempMaterial);
            }

            _tempMaterial.SetMatrix(ViewSpaceMatrix, matrix);
            _tempMaterial.SetTexture(ViewDepthTexture, perspectiveCamera.targetTexture);
            _tempMaterial.SetFloat(FieldOfView, visionCone.FieldOfView);
            _tempMaterial.SetColor(Color, color.Value);
            _tempMaterial.SetFloat(InnerRadius, innerRadius);
            _tempMaterial.SetFloat(RadiusSaturation, radiusSaturation);
            _tempMaterial.SetFloat(FieldOfViewSaturation, fieldOfViewSaturation);
        }
    }
}