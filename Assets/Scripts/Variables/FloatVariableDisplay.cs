using System.Globalization;

namespace Variables
{
    public class FloatVariableDisplay : VariableDisplay<float>
    {
        protected override void DisplayVariable(float value)
        {
            onDisplayValue?.Invoke(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}