using Cinemachine;
using Codetox.Variables;
using UnityEngine;

namespace Settings
{
    public class CameraSensitivityController : SettingController
    {
        private const string KeyX = "cam-sensitivity-x";
        private const float DefaultValueX = 0.25f;
        
        private const string KeyY = "cam-sensitivity-y";
        private const float DefaultValueY = 0.0025f;

        [SerializeField] private new CinemachineFreeLook camera;
        [SerializeField] private Variable<float> variableX;
        [SerializeField] private Variable<float> variableY;

        private void Awake()
        {
            variableX.Value = PlayerPrefs.HasKey(KeyX) ? PlayerPrefs.GetFloat(KeyX) : DefaultValueX;
            variableY.Value = PlayerPrefs.HasKey(KeyY) ? PlayerPrefs.GetFloat(KeyY) : DefaultValueY;
        }

        private void OnEnable()
        {
            variableX.OnValueChanged += SetSensitivityX;
            variableY.OnValueChanged += SetSensitivityY;
        }

        private void OnDisable()
        {
            variableX.OnValueChanged -= SetSensitivityX;
            variableY.OnValueChanged -= SetSensitivityY;
        }

        public void SetSensitivityX(float value)
        {
            SetCamera(value, variableY.Value);
            PlayerPrefs.SetFloat(KeyX, value);
            PlayerPrefs.Save();
        }

        public void SetSensitivityY(float value)
        {
            SetCamera(variableX.Value, value);
            PlayerPrefs.SetFloat(KeyY, value);
            PlayerPrefs.Save();
        }

        private void SetCamera(float x, float y)
        {
            if (!camera) return;
            camera.m_XAxis.m_MaxSpeed = x;
            camera.m_YAxis.m_MaxSpeed = y;
        }

        public override void ResetValue()
        {
            variableX.Value = DefaultValueX;
            variableY.Value = DefaultValueY;
        }
    }
}