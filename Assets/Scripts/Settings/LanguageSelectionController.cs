using System.Collections;
using System.Linq;
using Codetox.Variables;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Settings
{
    public class LanguageSelectionController : MonoBehaviour
    {
        [SerializeField] private RuntimeSet<string> options;
        [SerializeField] private Variable<int> index;

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            var locales = LocalizationSettings.AvailableLocales.Locales;
            var localesNames = locales.Select(locale => locale.LocaleName).ToList();

            options.Set(localesNames);
            index.Value = locales.IndexOf(LocalizationSettings.SelectedLocale);
        }

        private void OnEnable()
        {
            index.OnValueChanged += SetLanguage;
        }

        private void OnDisable()
        {
            index.OnValueChanged -= SetLanguage;
        }

        public void SetLanguage(int i)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
        }
    }
}