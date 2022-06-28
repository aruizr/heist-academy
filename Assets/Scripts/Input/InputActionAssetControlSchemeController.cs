using System.Linq;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Input
{
    public class InputActionAssetControlSchemeController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private Variable<InputControlScheme> currentControlScheme;

        private InputDevice _currentDevice;

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
            currentControlScheme.Value = inputActionAsset.controlSchemes.First(scheme => scheme.SupportsDevice(device));
        }
    }
}