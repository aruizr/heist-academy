using System.Linq;
using TMPro;
using UnityEngine;

namespace Settings
{
    public class GraphicsQualitySelectionController : MonoBehaviour
    {
        private const string Key = "quality-level";
        private const string Default = "default-quality-level";

        [SerializeField] private TMP_Dropdown dropdown;

        private void Awake()
        {
            var qualityLevels = QualitySettings.names.ToList();
            var level = QualitySettings.GetQualityLevel();
            
            PlayerPrefs.SetInt(Default, level);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                level = PlayerPrefs.GetInt(Key);
                QualitySettings.SetQualityLevel(level, true);
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(qualityLevels);
            dropdown.SetValueWithoutNotify(level);
            
        }

        public void SetQualityLevel(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public void ResetQualityLevel()
        {
            dropdown.value = PlayerPrefs.GetInt(Default);
        }
    }
}