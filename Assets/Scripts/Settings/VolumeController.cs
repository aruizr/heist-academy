using Codetox.Variables;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Settings
{
    public class VolumeController : SettingController
    {
        [SerializeField] private Variable<float> variable;
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

            variable.Value = value;
        }

        private void OnEnable()
        {
            variable.OnValueChanged += SetVolume;
        }

        private void OnDisable()
        {
            variable.OnValueChanged -= SetVolume;
        }

        public void SetVolume(float value)
        {
            mixer.SetVolume(parameterName, value);
            PlayerPrefs.SetFloat(parameterName, value);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            variable.Value = PlayerPrefs.GetFloat("default-" + parameterName);
        }
    }
}