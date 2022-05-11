using System;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private string parameterName;

        public void SetVolume(float value)
        {
            variable.Value = value;
            mixer.SetVolume(parameterName, value);
        }
    }
}