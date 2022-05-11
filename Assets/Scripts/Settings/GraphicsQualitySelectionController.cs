using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class GraphicsQualitySelectionController : MonoBehaviour
    {
        private const string Key = "quality-level";

        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            var qualityLevels = QualitySettings.names.ToList();

            dropdown.ClearOptions();
            dropdown.AddOptions(qualityLevels);

            if (!PlayerPrefs.HasKey(Key))
            {
                dropdown.value = QualitySettings.GetQualityLevel();
                return;
            }

            var level = PlayerPrefs.GetInt(Key);

            dropdown.value = level;
            QualitySettings.SetQualityLevel(level, true);
        }

        public void SetQualityLevel(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
            PlayerPrefs.SetInt(Key, QualitySettings.GetQualityLevel());
            PlayerPrefs.Save();
        }
    }
}