using Codetox.Variables;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private FloatVariable variable;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName;

        private void Awake()
        {
            slider.value = variable.Value;
            mixer.SetVolume(parameterName, variable.Value);
        }

        public void SetVolume(float value)
        {
            variable.Value = value;
            mixer.SetVolume(parameterName, value);
        }
    }
}