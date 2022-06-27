using System.Collections;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Settings
{
    public class LanguageSelectionController : MonoBehaviour
    {
        [SerializeField] private OptionSelector selector;

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            var locales = LocalizationSettings.AvailableLocales.Locales;
            var localesNames = locales.Select(locale => locale.LocaleName).ToList();

            selector.SetOptions(localesNames);
            selector.SetValueWithoutNotify(locales.IndexOf(LocalizationSettings.SelectedLocale));
        }

        public void SetLanguage(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}