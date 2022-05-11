using System.Linq;
using Codetox.Variables;
using TMPro;
using UnityEngine;
using Utilities;

namespace Settings
{
    public class ScreenResolutionSelectionController : MonoBehaviour
    {
        private const string Key = "screen-resolution";
        
        [SerializeField] private TMP_Dropdown dropdown;

        private void Awake()
        {
            var resolutions = Screen.resolutions.ToList();
            var resolutionStrings = Screen.resolutions.Select(res => $"{res.width} x {res.height}").ToList();
            var index = PlayerPrefs.HasKey(Key) ? PlayerPrefs.GetInt(Key) : resolutions.IndexOf(Screen.currentResolution);

            dropdown.ClearOptions();
            dropdown.AddOptions(resolutionStrings);
            dropdown.value = resolutionIndex.Value;
        }

        public void SetResolution(int index)
        {
            var resolution = Screen.resolutions[index];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            resolutionIndex.Value = index;
        }
    }
}