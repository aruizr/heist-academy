using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Settings
{
    public class LanguageSelectionController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            var locales = LocalizationSettings.AvailableLocales.Locales;
            var localesNames = locales.Select(locale => locale.LocaleName).ToList();

            dropdown.ClearOptions();
            dropdown.AddOptions(localesNames);
            dropdown.SetValueWithoutNotify(locales.IndexOf(LocalizationSettings.SelectedLocale));
        }

        public void SetLanguage(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}