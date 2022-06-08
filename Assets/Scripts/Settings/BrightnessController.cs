using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class BrightnessController : MonoBehaviour
    {
        private const string Key = "brightness";
        private const string Default = "default-brightness";

        [SerializeField] private Slider slider;

        private void Awake()
        {
            var value = Screen.brightness;

            PlayerPrefs.SetFloat(Default, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetFloat(Key);
                Screen.brightness = value;
            }

            slider.SetValueWithoutNotify(value);
        }

        public void SetBrightness(float value)
        {
            Screen.brightness = value;
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public void ResetBrightness()
        {
            slider.value = PlayerPrefs.GetFloat(Default);
        }
    }
}