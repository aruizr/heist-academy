using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class ScreenResolutionSelectionController : MonoBehaviour
    {
        private const string Key = "screen-resolution";

        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            var resolutions = Screen.resolutions.ToList();
            var resolutionStrings = Screen.resolutions.Select(res => $"{res.width} x {res.height}").ToList();

            dropdown.ClearOptions();
            dropdown.AddOptions(resolutionStrings);

            if (!PlayerPrefs.HasKey(Key))
            {
                dropdown.value = resolutions.IndexOf(Screen.currentResolution);
                return;
            }

            var index = PlayerPrefs.GetInt(Key);
            var resolution = resolutions[index];

            dropdown.value = index;
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
        }

        public void SetResolution(int index)
        {
            var resolution = Screen.resolutions[index];
            
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }
    }
}