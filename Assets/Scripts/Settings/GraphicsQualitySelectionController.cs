using System.Linq;
using Codetox.Variables;
using TMPro;
using UnityEngine;
using Utilities;

namespace Settings
{
    public class GraphicsQualitySelectionController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private IntVariable qualityLevel;

        private void Awake()
        {
            var qualityLevels = QualitySettings.names.ToList();
            var qualityLevelRange = new Range<int>(0, qualityLevels.Count - 1);

            if (qualityLevelRange.IsInRange(qualityLevel.Value))
            {
                QualitySettings.SetQualityLevel(qualityLevel.Value, true);
            }
            else
            {
                qualityLevel.Value = QualitySettings.GetQualityLevel();
            }

            dropdown.ClearOptions();
            dropdown.AddOptions(qualityLevels);
            dropdown.value = qualityLevel.Value;
        }

        public void SetQualityLevel(int index)
        {
            qualityLevel.Value = index;
            QualitySettings.SetQualityLevel(index, true);
        }
    }
}