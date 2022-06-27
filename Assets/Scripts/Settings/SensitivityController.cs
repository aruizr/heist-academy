using Codetox.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Settings
{
    public class SensitivityController : SettingController
    {
        private const string Key = "sensitivity";
        private const string Default = "default-sensitivity";

        [SerializeField] private Slider slider;
        [SerializeField] private InputActionReference moveCameraActionReference;

        private void Awake()
        {
            var value = 1f;

            PlayerPrefs.SetFloat(Default, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetFloat(Key);
                moveCameraActionReference.action.ApplyBindingOverride(new InputBinding {overrideProcessors = $"ScaleVector2(x={value},y={value})"});
            }

            slider.SetValueWithoutNotify(value);
        }

        public void SetSensitivity(float value)
        {
            moveCameraActionReference.action.ApplyBindingOverride(new InputBinding {overrideProcessors = $"ScaleVector2(x={value},y={value})"});
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            slider.value = PlayerPrefs.GetFloat(Default);
        }
    }
}