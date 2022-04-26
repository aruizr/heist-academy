using System;
using Codetox.Variables;
using MyBox;
using UnityEngine;

namespace Variables
{
    [Serializable]
    public class ValueReference<T>
    {
        [SerializeField] private bool useVariable;

        [SerializeField] [ConditionalField(nameof(useVariable))]
        private Variable<T> variableValue;

        [SerializeField] [ConditionalField(nameof(useVariable), true)]
        private T fieldValue;

        public T Value
        {
            get
            {
                if (useVariable && variableValue) return variableValue.Value;
                return fieldValue;
            }
            set
            {
                if (useVariable && variableValue) variableValue.Value = value;
                else fieldValue = value;
            }
        }
    }
}