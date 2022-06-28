using Codetox.Variables;
using UnityEngine;

namespace Settings
{
    public class BrightnessController : SettingController
    {
        private const string Key = "brightness";
        private const string Default = "default-brightness";

        [SerializeField] private Variable<float> variable;

        private void Start()
        {
            var value = Screen.brightness;

            PlayerPrefs.SetFloat(Default, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetFloat(Key);
                Screen.brightness = value;
            }

            variable.Value = value;
        }
        
        private void OnEnable()
        {
            variable.OnValueChanged += SetBrightness;
        }

        private void OnDisable()
        {
            variable.OnValueChanged -= SetBrightness;
        }

        public void SetBrightness(float value)
        {
            Screen.brightness = value;
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            variable.Value = PlayerPrefs.GetFloat(Default);
        }
    }
}