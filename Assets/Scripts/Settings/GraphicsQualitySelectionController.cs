using System.Linq;
using UI;
using UnityEngine;

namespace Settings
{
    public class GraphicsQualitySelectionController : SettingController
    {
        private const string Key = "quality-level";
        private const string Default = "default-quality-level";

        [SerializeField] private OptionSelector selector;

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

            selector.SetOptions(qualityLevels);
            selector.SetValueWithoutNotify(level);
        }

        public void SetQualityLevel(int index)
        {
            QualitySettings.SetQualityLevel(index, true);
            PlayerPrefs.SetInt(Key, index);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            selector.value = PlayerPrefs.GetInt(Default);
        }
    }
}