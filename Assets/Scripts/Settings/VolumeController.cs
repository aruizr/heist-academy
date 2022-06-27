using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VolumeController : SettingController
    {
        [SerializeField] private Slider slider;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName;

        private void Start()
        {
            mixer.GetVolume(parameterName, out var value);
            
            PlayerPrefs.SetFloat("default-" + parameterName, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(parameterName))
            {
                value = PlayerPrefs.GetFloat(parameterName);
                mixer.SetVolume(parameterName, value);
            }
            
            slider.SetValueWithoutNotify(value);
        }

        public void SetVolume(float value)
        {
            mixer.SetVolume(parameterName, value);
            PlayerPrefs.SetFloat(parameterName, value);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            slider.value = PlayerPrefs.GetFloat("default-" + parameterName);
        }
    }
}