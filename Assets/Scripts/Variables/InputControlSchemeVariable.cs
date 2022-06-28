using Codetox.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Variables
{
    [CreateAssetMenu(fileName = nameof(InputControlSchemeVariable), menuName = "Trashy Games/Variables/Input Control Scheme")]
    public class InputControlSchemeVariable: Variable<InputControlScheme>
    {
    }
}