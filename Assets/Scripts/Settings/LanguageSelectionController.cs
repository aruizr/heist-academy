using System.Linq;
using Codetox.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Utilities;

namespace Settings
{
    public class LanguageSelectionController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private IntVariable localeIndex;

        private void Awake()
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            var localesNames = locales.Select(locale => locale.LocaleName).ToList();
            var localesRange = new Range<int>(0, locales.Count - 1);

            if (localesRange.IsInRange(localeIndex.Value))
                LocalizationSettings.SelectedLocale = locales[localeIndex.Value];
            else
                localeIndex.Value = locales.IndexOf(LocalizationSettings.SelectedLocale);

            dropdown.ClearOptions();
            dropdown.AddOptions(localesNames);
            dropdown.value = localeIndex.Value;
        }

        public void SetLanguage(int index)
        {
            localeIndex.Value = index;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}