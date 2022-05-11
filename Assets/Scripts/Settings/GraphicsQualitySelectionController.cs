using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class GraphicsQualitySelectionController : MonoBehaviour
    {
        private const string Key = "quality-level";

        [SerializeField] private TMP_Dropdown dropdown;

        private void Awake()
        {
            var qualityLevels = QualitySettings.names.ToList();

            if (PlayerPrefs.HasKey(Key)) QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(Key), true);

            dropdown.ClearOptions();
            dropdown.AddOptions(qualityLevels);
            dropdown.value = QualitySettings.GetQualityLevel();
        }

        public void SetQualityLevel(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
            PlayerPrefs.SetInt(Key, QualitySettings.GetQualityLevel());
        }
    }
}