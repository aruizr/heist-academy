using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class ScreenResolutionSelectionController : MonoBehaviour
    {
        private const string Key = "screen-resolution";
        private const string Default = "default-screen-resolution";

        [SerializeField] private TMP_Dropdown dropdown;

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
            
            dropdown.ClearOptions();
            dropdown.AddOptions(resolutionStrings);
            dropdown.SetValueWithoutNotify(index);
        }

        public void SetResolution(int index)
        {
            var resolution = Screen.resolutions[index];
            
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public void ResetResolution()
        {
            dropdown.value = PlayerPrefs.GetInt(Default);
        }
    }
}