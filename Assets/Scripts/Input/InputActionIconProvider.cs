using System;
using Codetox.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionIconProvider : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputIconMap inputIconMap;
        [SerializeField] private TMP_SpriteAsset spriteAsset;
        [SerializeField] private Variable<InputControlScheme> currentControlScheme;

        private void OnEnable()
        {
            currentControlScheme.OnValueChanged += OnControlSchemeChanged;
        }

        private void OnDisable()
        {
            currentControlScheme.OnValueChanged -= OnControlSchemeChanged;
        }

        private void OnControlSchemeChanged(InputControlScheme inputControlScheme)
        {
            UpdateIcon();
        }

        public void UpdateIcon()
        {
            try
            {
                var inputAction = inputActionReference.action;
                var bindingIndex = inputAction.GetBindingIndex(currentControlScheme.Value.bindingGroup);
                var binding = inputAction.bindings[bindingIndex];
                var path = binding.hasOverrides ? binding.overridePath : binding.path;
                var sprite = inputIconMap.GetIcon(path);
                
                spriteAsset.material.SetTexture(ShaderUtilities.ID_MainTex, sprite.texture);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}