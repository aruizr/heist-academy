using Codetox.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleController : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private Variable<bool> variable;

        private void OnEnable()
        {
            if (!variable) return;

            toggle.isOn = variable.Value;

            toggle.onValueChanged.AddListener(OnToggleValueChanged);
            variable.OnValueChanged += OnVariableValueChanged;
        }

        private void OnDisable()
        {
            if (!variable) return;

            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            variable.OnValueChanged -= OnVariableValueChanged;
        }

        private void OnVariableValueChanged(bool value)
        {
            toggle.isOn = value;
        }

        private void OnToggleValueChanged(bool value)
        {
            variable.Value = value;
        }
    }
}