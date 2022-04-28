using System;
using Codetox.Variables;

namespace Variables
{
    [Serializable]
    public class ValueReference<T>
    {
        public bool useField = true;
        public T fieldValue;
        public Variable<T> variableValue;

        public T Value
        {
            get
            {
                if (!useField && variableValue) return variableValue.Value;
                return fieldValue;
            }
            set
            {
                if (!useField && variableValue) variableValue.Value = value;
                else fieldValue = value;
            }
        }
    }
}