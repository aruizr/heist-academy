using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Variables;

namespace Settings
{
    public class WindowModeSelectionController : MonoBehaviour
    {
        private const string WindowModeKey = "Window Mode";
        
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private WindowModeEnumVariable windowMode;

        private readonly Dictionary<FullScreenMode, string> _screenModeNames = new Dictionary<FullScreenMode, string>
        {
            {FullScreenMode.ExclusiveFullScreen, "Full screen"},
            {FullScreenMode.FullScreenWindow, "Borderless full screen"},
            {FullScreenMode.MaximizedWindow, "Maximized window"},
            {FullScreenMode.Windowed, "Windowed"}
        };

        private void Awake()
        {
            var windowModeNames = _screenModeNames.Values.ToList();
            var currentWindowModeName = _screenModeNames[windowMode.Value];
            var currentWindowModeIndex = windowModeNames.IndexOf(currentWindowModeName);

            dropdown.ClearOptions();
            dropdown.AddOptions(windowModeNames);
            dropdown.value = currentWindowModeIndex;
            Screen.fullScreenMode = windowMode.Value;
        }

        public void SetWindowMode(int index)
        {
            var selectedWindowModeName = dropdown.options[index].text;
            var selectedWindowMode = _screenModeNames.First(pair => pair.Value == selectedWindowModeName).Key;
            
            Screen.fullScreenMode = selectedWindowMode;
            windowMode.Value = selectedWindowMode;
        }
    }
}