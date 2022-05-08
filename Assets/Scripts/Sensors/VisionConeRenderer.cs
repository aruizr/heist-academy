using Codetox.Messaging;
using UnityEngine;

namespace Sensors
{
    public class VisionConeRenderer : MonoBehaviour
    {
        [SerializeField] private VisionCone visionCone;
        [SerializeField] private Color color = Color.white;
        [SerializeField] [Range(0, 1)] private float innerRadius;
        [SerializeField] [Min(0)] private float radiusSaturation = 10;
        [SerializeField] [Min(0)] private float fieldOfViewSaturation = 10;
        [SerializeField] [Min(0)] private float shaderObjectHeight = 2;
        
        private Material _shaderMaterial;
        private GameObject _shaderObject;
        private Camera _visionCamera;

        private void Awake()
        {
            _visionCamera = gameObject.AddComponent<Camera>();
            _visionCamera.enabled = false;
            _shaderObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _shaderObject.transform.SetParent(transform, false);
            _shaderObject.Send<Collider>(Destroy);
            _shaderMaterial = new Material(Shader.Find("Custom/Vision Cone Shader"));
            _shaderObject.Send<MeshRenderer>(meshRenderer => meshRenderer.material = _shaderMaterial);
        }

        private void Update()
        {
            if (!visionCone) return;
            if (!_visionCamera) return;
            if (!_shaderObject) return;
            if (!_shaderMaterial) return;

            SetVisionCameraProperties();
            SetShaderProperties();
            SetShaderObjectProperties();
        }

        private void SetVisionCameraProperties()
        {
            _visionCamera.farClipPlane = visionCone.Distance;
            _visionCamera.fieldOfView = visionCone.FieldOfView;

            if (!_visionCamera.targetTexture || _visionCamera.targetTexture.width != _visionCamera.pixelWidth ||
                _visionCamera.targetTexture.height != _visionCamera.pixelHeight)
                _visionCamera.targetTexture = new RenderTexture(
                    _visionCamera.pixelWidth,
                    _visionCamera.pixelHeight,
                    24,
                    RenderTextureFormat.Depth);

            _visionCamera.Render();
        }

        private void SetShaderObjectProperties()
        {
            var horizontalScale = visionCone.Distance * 2;

            _shaderObject.transform.localScale = new Vector3(horizontalScale, shaderObjectHeight, horizontalScale);
            _shaderObject.transform.localEulerAngles = new Vector3(0, -90, 0);
        }

        private void SetShaderProperties()
        {
            var matrix = _visionCamera.projectionMatrix * _visionCamera.worldToCameraMatrix;

            _shaderMaterial.SetMatrix("_ViewSpaceMatrix", matrix);
            _shaderMaterial.SetTexture("_ViewDepthTexture", _visionCamera.targetTexture);
            _shaderMaterial.SetFloat("_FieldOfView", visionCone.FieldOfView);
            _shaderMaterial.SetColor("_Color", color);
            _shaderMaterial.SetFloat("_InnerRadius", innerRadius);
            _shaderMaterial.SetFloat("_RadiusSaturation", radiusSaturation);
            _shaderMaterial.SetFloat("_FieldOfViewSaturation", fieldOfViewSaturation);
        }
    }
}