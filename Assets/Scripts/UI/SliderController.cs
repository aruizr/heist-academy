using Codetox.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Variable<float> variable;

        private void OnEnable()
        {
            if (!variable) return;

            slider.value = variable.Value;

            slider.onValueChanged.AddListener(OnSliderValueChanged);
            variable.OnValueChanged += OnVariableValueChanged;
        }

        private void OnDisable()
        {
            if (!variable) return;

            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            variable.OnValueChanged -= OnVariableValueChanged;
        }

        private void OnVariableValueChanged(float value)
        {
            slider.value = value;
        }

        private void OnSliderValueChanged(float value)
        {
            variable.Value = value;
        }
    }
}