using System.Collections.Generic;
using System.Linq;
using Codetox.Variables;
using RuntimeSets;
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

        [SerializeField] private RuntimeSet<string> options;
        [SerializeField] private Variable<int> index;

        private void Awake()
        {
            var i = ScreenModes.IndexOf(Screen.fullScreenMode);

            PlayerPrefs.SetInt(Default, i);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                i = PlayerPrefs.GetInt(Key);
                Screen.fullScreenMode = ScreenModes[i];
            }

            options.Set(ScreenModeNames.ToList());
            index.Value = i;
        }

        private void OnEnable()
        {
            index.OnValueChanged += SetWindowMode;
        }

        private void OnDisable()
        {
            index.OnValueChanged -= SetWindowMode;
        }

        public void SetWindowMode(int i)
        {
            Screen.fullScreenMode = ScreenModes[i];
            PlayerPrefs.SetInt(Key, i);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            index.Value = PlayerPrefs.GetInt(Default);
        }
    }
}