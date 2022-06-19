using Codetox.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SensitivityController : MonoBehaviour
    {
        private const string Key = "sensitivity";
        private const string Default = "default-sensitivity";

        [SerializeField] private Slider slider;
        [SerializeField] private FloatVariable sensitivity;

        private void Awake()
        {
            var value = sensitivity.Value;

            PlayerPrefs.SetFloat(Default, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetFloat(Key);
                sensitivity.Value = value;
            }

            slider.SetValueWithoutNotify(value);
        }

        public void SetSensitivity(float value)
        {
            sensitivity.Value = value;
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public void ResetSensitivity()
        {
            slider.value = PlayerPrefs.GetFloat(Default);
        }
    }
}