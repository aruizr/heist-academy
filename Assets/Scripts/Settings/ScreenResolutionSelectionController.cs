using System.Linq;
using UI;
using UnityEngine;

namespace Settings
{
    public class ScreenResolutionSelectionController : SettingController
    {
        private const string Key = "screen-resolution";
        private const string Default = "default-screen-resolution";

        [SerializeField] private OptionSelector selector;

        private void Awake()
        {
            var resolutions = Screen.resolutions.ToList();
            var resolutionStrings = Screen.resolutions.Select(res => $"{res.width} x {res.height}").ToList();
            var index = resolutions.IndexOf(Screen.currentResolution);

            PlayerPrefs.SetInt(Default, index);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                index = PlayerPrefs.GetInt(Key);
                var resolution = resolutions[index];
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            }

            selector.SetOptions(resolutionStrings);
            selector.SetValueWithoutNotify(index);
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
            selector.value = PlayerPrefs.GetInt(Default);
        }
    }
}