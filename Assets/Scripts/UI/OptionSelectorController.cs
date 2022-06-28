using System.Linq;
using Codetox.Variables;
using RuntimeSets;
using UnityEngine;

namespace UI
{
    public class OptionSelectorController : MonoBehaviour
    {
        [SerializeField] private OptionSelector selector;
        [SerializeField] private RuntimeSet<string> options;
        [SerializeField] private Variable<int> index;

        private void OnEnable()
        {
            selector.SetOptions(options.ToList());
            selector.SetValueWithoutNotify(index.Value);

            selector.onValueChanged.AddListener(OnSelectorValueChanged);
            index.OnValueChanged += OnIndexValueChanged;
        }

        private void OnDisable()
        {
            selector.onValueChanged.RemoveListener(OnSelectorValueChanged);
            index.OnValueChanged -= OnIndexValueChanged;
        }

        private void OnIndexValueChanged(int i)
        {
            selector.SetValueWithoutNotify(i);
        }

        private void OnSelectorValueChanged(int i)
        {
            index.Value = i;
        }
    }
}