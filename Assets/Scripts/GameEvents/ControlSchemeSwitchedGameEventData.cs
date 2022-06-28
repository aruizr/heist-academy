using UnityEngine.InputSystem;

namespace GameEvents
{
    public class ControlSchemeSwitchedGameEventData
    {
        public ControlSchemeSwitchedGameEventData(InputDevice inputDevice, InputControlScheme controlScheme)
        {
            Device = inputDevice;
            ControlScheme = controlScheme;
        }

        public InputDevice Device { get; }

        public InputControlScheme ControlScheme { get; }
    }
}