using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class WindowModeSelectionController : MonoBehaviour
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

        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            var index = ScreenModes.IndexOf(Screen.fullScreenMode);

            PlayerPrefs.SetInt(Default, index);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                index = PlayerPrefs.GetInt(Key);
                Screen.fullScreenMode = ScreenModes[index];
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(ScreenModeNames.ToList());
            dropdown.SetValueWithoutNotify(index);
        }

        public void SetWindowMode(int index)
        {
            Screen.fullScreenMode = ScreenModes[index];
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public void ResetWindowMode()
        {
            dropdown.value = PlayerPrefs.GetInt(Default);
        }
    }
}