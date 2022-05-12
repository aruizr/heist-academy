using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName;

        private void Start()
        {
            float value;

            if (!PlayerPrefs.HasKey(parameterName))
            {
                mixer.GetFloat(parameterName, out value);
                slider.value = value;
                return;
            }

            value = PlayerPrefs.GetFloat(parameterName);

            slider.value = value;
            mixer.SetVolume(parameterName, value);
        }

        public void SetVolume(float value)
        {
            mixer.SetVolume(parameterName, value);
            PlayerPrefs.SetFloat(parameterName, value);
            PlayerPrefs.Save();
        }
    }
}