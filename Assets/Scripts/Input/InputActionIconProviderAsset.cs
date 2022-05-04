using System;
using System.Linq;
using Codetox;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Input
{
    [CreateAssetMenu(fileName = nameof(InputActionIconProviderAsset),
        menuName = "Trashy Games/Input/Icon Provider", order = 0)]
    public class InputActionIconProviderAsset : CustomScriptableObject
    {
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputIconMap inputIconMap;
        [SerializeField] private TMP_SpriteAsset spriteAsset;

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
            if (!inputActionReference || !inputIconMap || !spriteAsset) return;

            if (_currentDevice != null && _currentDevice == device) return;
            if (eventPtr.type == StateEvent.Type)
                if (!eventPtr.EnumerateChangedControls(device, 0.001f).Any())
                    return;

            _currentDevice = device;
            _currentScheme =
                inputActionReference.asset.controlSchemes.First(scheme => scheme.SupportsDevice(_currentDevice));
            
            try
            {
                var inputAction = inputActionReference.action;
                var currentBindingIndex = inputAction.GetBindingIndex(_currentScheme.bindingGroup);
                var currentBinding = inputAction.bindings[currentBindingIndex];
                var currentSprite = inputIconMap.GetIcon(currentBinding.path);
                spriteAsset.material.SetTexture(ShaderUtilities.ID_MainTex, currentSprite.texture);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}