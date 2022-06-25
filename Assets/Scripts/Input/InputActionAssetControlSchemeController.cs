using System.Linq;
using GameEvents;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Input
{
    public class InputActionAssetControlSchemeController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private ControlSchemeSwitchedGameEvent gameEvent;

        private InputDevice _currentDevice;
        private InputControlScheme _currentScheme;

        private void OnEnable()
        {
            InputSystem.onEvent += OnInputSystemEvent;
        }

        private void OnDisable()
        {
            InputSystem.onEvent -= OnInputSystemEvent;
        }

        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (!inputActionAsset) return;

            if (_currentDevice != null && _currentDevice == device) return;
            if (eventPtr.type == StateEvent.Type)
                if (!eventPtr.EnumerateChangedControls(device, 0.001f).Any())
                    return;

            _currentDevice = device;
            _currentScheme = inputActionAsset.controlSchemes.First(scheme => scheme.SupportsDevice(_currentDevice));

            var eventData = new ControlSchemeSwitchedGameEventData(_currentDevice, _currentScheme);

            gameEvent.Invoke(eventData);
        }
    }
}