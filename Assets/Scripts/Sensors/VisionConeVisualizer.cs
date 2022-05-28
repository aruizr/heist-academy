using Codetox.Messaging;
using UnityEngine;

namespace Sensors
{
    [ExecuteAlways]
    public class VisionConeVisualizer : MonoBehaviour
    {
        [SerializeField] private VisionCone visionCone;
        [SerializeField] private GameObject shaderObject;
        [SerializeField] private Material shaderMaterial;
        [SerializeField] private Camera perspectiveCamera;
        [SerializeField] private Color color = Color.white;
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

            _tempMaterial.SetMatrix("_ViewSpaceMatrix", matrix);
            _tempMaterial.SetTexture("_ViewDepthTexture", perspectiveCamera.targetTexture);
            _tempMaterial.SetFloat("_FieldOfView", visionCone.FieldOfView);
            _tempMaterial.SetColor("_Color", color);
            _tempMaterial.SetFloat("_InnerRadius", innerRadius);
            _tempMaterial.SetFloat("_RadiusSaturation", radiusSaturation);
            _tempMaterial.SetFloat("_FieldOfViewSaturation", fieldOfViewSaturation);
        }
    }
}