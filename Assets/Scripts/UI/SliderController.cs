using System;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private FloatVariable variable;

        private void Awake()
        {
            slider.value = variable.Value;
        }

        private void OnEnable()
        {
            variable.OnValueChanged += VariableOnOnValueChanged;
            slider.onValueChanged.AddListener(SliderOnValueChanged);
        }

        private void OnDisable()
        {
            variable.OnValueChanged -= VariableOnOnValueChanged;
            slider.onValueChanged.RemoveListener(SliderOnValueChanged);
        }

        private void SliderOnValueChanged(float value)
        {
            if (Math.Abs(variable.Value - value) < 0.001) return;
            variable.Value = value;
        }

        private void VariableOnOnValueChanged(float value)
        {
            if (Math.Abs(slider.value - value) < 0.001) return;
            slider.value = value;
        }
    }
}