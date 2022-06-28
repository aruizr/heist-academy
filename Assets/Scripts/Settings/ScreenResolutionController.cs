using System;
using System.Linq;
using Codetox.Variables;
using RuntimeSets;
using UnityEngine;

namespace Settings
{
    public class ScreenResolutionController : SettingController
    {
        private const string Key = "screen-resolution";
        private const string Default = "default-screen-resolution";

        [SerializeField] private RuntimeSet<string> options;
        [SerializeField] private Variable<int> index;

        private void Awake()
        {
            var resolutions = Screen.resolutions.ToList();
            var resolutionStrings = Screen.resolutions.Select(res => $"{res.width} x {res.height}").ToList();
            var i = resolutions.IndexOf(Screen.currentResolution);

            PlayerPrefs.SetInt(Default, i);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                i = PlayerPrefs.GetInt(Key);
                var resolution = resolutions[i];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            }

            options.Set(resolutionStrings);
            index.Value = i;
        }

        private void OnEnable()
        {
            index.OnValueChanged += SetResolution;
        }

        private void OnDisable()
        {
            index.OnValueChanged -= SetResolution;
        }

        public void SetResolution(int index)
        {
            var resolution = Screen.resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            index.Value = PlayerPrefs.GetInt(Default);
        }
    }
}