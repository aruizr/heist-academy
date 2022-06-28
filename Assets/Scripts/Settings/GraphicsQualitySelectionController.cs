using System.Linq;
using Codetox.Variables;
using RuntimeSets;
using UI;
using UnityEngine;

namespace Settings
{
    public class GraphicsQualitySelectionController : SettingController
    {
        private const string Key = "quality-level";
        private const string Default = "default-quality-level";

        [SerializeField] private RuntimeSet<string> options;
        [SerializeField] private Variable<int> index;

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

            options.Set(qualityLevels);
            index.Value = level;
        }
        
        private void OnEnable()
        {
            index.OnValueChanged += SetQualityLevel;
        }

        private void OnDisable()
        {
            index.OnValueChanged -= SetQualityLevel;
        }

        public void SetQualityLevel(int level)
        {
            QualitySettings.SetQualityLevel(level, true);
            PlayerPrefs.SetInt(Key, level);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            index.Value = PlayerPrefs.GetInt(Default);
        }
    }
}