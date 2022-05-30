using Codetox.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Variables
{
    public abstract class VariableDisplay<T> : MonoBehaviour
    {
        [SerializeField] private Variable<T> variable;

        public UnityEvent<string> onDisplayValue;

        private void OnEnable()
        {
            DisplayVariable(variable.Value);
            variable.OnValueChanged += DisplayVariable;
        }

        private void OnDisable()
        {
            variable.OnValueChanged -= DisplayVariable;
        }

        protected virtual void DisplayVariable(T value)
        {
            onDisplayValue?.Invoke(value.ToString());
        }
    }
}