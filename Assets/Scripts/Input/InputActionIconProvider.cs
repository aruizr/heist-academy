using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Input
{
    public class InputActionIconProvider : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputIconMap inputIconMap;
        [SerializeField] private TMP_Text text;

        private InputDevice _currentDevice;
        private InputControlScheme _currentScheme;

        private void Awake()
        {
            InputSystem.onEvent += OnInputSystemEvent;
        }

        private void OnInputSystemEvent(InputEventPtr eventPtr, InputDevice device)
        {
            if (_currentDevice != null && _currentDevice == device) return;
            if (eventPtr.type == StateEvent.Type)
            {
                if (!eventPtr.EnumerateChangedControls(device, 0.001f).Any())
                {
                    return;
                }
            }

            _currentDevice = device;
            _currentScheme = inputActionReference.asset.controlSchemes.
                First(scheme => scheme.SupportsDevice(_currentDevice));
            
            var inputAction = inputActionReference.action;
            var currentBindingIndex = inputAction.GetBindingIndex(_currentScheme.bindingGroup);
            var currentBinding = inputAction.bindings[currentBindingIndex];
            var currentSprite = inputIconMap.GetIcon(currentBinding.path);
            var spriteAsset = text.spriteAsset;
            
            spriteAsset.material.SetTexture(ShaderUtilities.ID_MainTex, currentSprite.texture);
            text.ForceMeshUpdate();
        }
    }
}