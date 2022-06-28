using System.Globalization;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Settings
{
    public class SensitivityController : SettingController
    {
        private const string KeyX = "sensitivity-x";
        private const string KeyY = "sensitivity-y";

        [SerializeField] private Variable<float> variableX;
        [SerializeField] private Variable<float> variableY;
        [SerializeField] private InputActionReference moveCameraActionReference;

        private void Start()
        {
            var valueX = 1f;
            var valueY = 1f;

            if (PlayerPrefs.HasKey(KeyX))
            {
                valueX = PlayerPrefs.GetFloat(KeyX);
            }
            
            if (PlayerPrefs.HasKey(KeyY))
            {
                valueY = PlayerPrefs.GetFloat(KeyY);
            }
            
            SetInputSensitivity(valueX, valueY);

            variableX.Value = valueX;
            variableY.Value = valueY;
        }

        private void OnEnable()
        {
            variableX.OnValueChanged += SetSensitivityX;
            variableY.OnValueChanged += SetSensitivityY;
        }

        private void OnDisable()
        {
            variableX.OnValueChanged -= SetSensitivityX;
            variableY.OnValueChanged += SetSensitivityY;
        }

        public void SetSensitivityX(float value)
        {
            SetInputSensitivity(value, variableY.Value);
            PlayerPrefs.SetFloat(KeyX, value);
            PlayerPrefs.Save();
        }
        
        public void SetSensitivityY(float value)
        {
            SetInputSensitivity(variableX.Value, value);
            PlayerPrefs.SetFloat(KeyY, value);
            PlayerPrefs.Save();
        }

        private void SetInputSensitivity(float x, float y)
        {
            var convertedX = x.ToString(CultureInfo.InvariantCulture);
            var convertedY = y.ToString(CultureInfo.InvariantCulture);
            var bindingOverride = new InputBinding {overrideProcessors = $"scaleVector2(x={convertedX},y={convertedY})"};
            
            moveCameraActionReference.action.ApplyBindingOverride(bindingOverride);
        }

        public override void ResetValue()
        {
            variableX.Value = 1f;
            variableY.Value = 1f;
        }
    }
}