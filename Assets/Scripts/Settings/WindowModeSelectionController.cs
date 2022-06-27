using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

namespace Settings
{
    public class WindowModeSelectionController : SettingController
    {
        private const string Key = "window-mode";
        private const string Default = "default-window-mode";

        private static readonly List<string> ScreenModeNames = new List<string>
        {
            "Full screen", "Borderless full screen", "Maximized window", "Windowed"
        };

        private static readonly List<FullScreenMode> ScreenModes = new List<FullScreenMode>
        {
            FullScreenMode.ExclusiveFullScreen, FullScreenMode.FullScreenWindow, FullScreenMode.MaximizedWindow,
            FullScreenMode.Windowed
        };

        [SerializeField] private OptionSelector selector;

        private void Awake()
        {
            var index = ScreenModes.IndexOf(Screen.fullScreenMode);

            PlayerPrefs.SetInt(Default, index);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                index = PlayerPrefs.GetInt(Key);
                Screen.fullScreenMode = ScreenModes[index];
            }

            selector.SetOptions(ScreenModeNames.ToList());
            selector.SetValueWithoutNotify(index);
        }

        public void SetWindowMode(int index)
        {
            Screen.fullScreenMode = ScreenModes[index];
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            selector.value = PlayerPrefs.GetInt(Default);
        }
    }
}