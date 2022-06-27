using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

namespace UI
{
    public class SliderNumberController : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Slider slider;
        [SerializeField] [Min(0)] private int decimals;
        [SerializeField] [Min(0)] private float multiplier = 1;
        [SerializeField] private LocalizedString localizedString;

        private void Awake()
        {
            UpdateText(slider.value);
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(UpdateText);
            localizedString.StringChanged += OnLocalizedStringOnValueChanged;
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(UpdateText);
            localizedString.StringChanged -= OnLocalizedStringOnValueChanged;
        }

        private void OnLocalizedStringOnValueChanged(string value)
        {
            UpdateText(slider.value);
        }

        private void UpdateText(float value)
        {
            var n = Mathf.Pow(10f, decimals);
            var result = Mathf.Round(value * n) / n;
            var final = result * multiplier;
            text.text = final.ToString(LocalizationSettings.SelectedLocale.Formatter);
        }
    }
}