using System;
using Codetox.GameEvents;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionIconProvider : MonoBehaviour
    {
        [SerializeField] private ControlSchemeSwitchedGameEvent gameEvent;
        [SerializeField] private VoidGameEvent updateIconGameEvent;
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputIconMap inputIconMap;
        [SerializeField] private TMP_SpriteAsset spriteAsset;

        private string _currentBindingGroup;

        private void OnEnable()
        {
            gameEvent.AddListener(OnControlSchemeSwitched);
            updateIconGameEvent.AddListener(UpdateIcon);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(OnControlSchemeSwitched);
            updateIconGameEvent.RemoveListener(UpdateIcon);
        }

        private void OnControlSchemeSwitched(ControlSchemeSwitchedGameEventData data)
        {
            _currentBindingGroup = data.ControlScheme.bindingGroup;
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            try
            {
                var inputAction = inputActionReference.action;
                var currentBindingIndex = inputAction.GetBindingIndex(_currentBindingGroup);
                var currentBinding = inputAction.bindings[currentBindingIndex];
                var currentSprite = inputIconMap.GetIcon(currentBinding.hasOverrides ? currentBinding.overridePath : currentBinding.path);
                spriteAsset.material.SetTexture(ShaderUtilities.ID_MainTex, currentSprite.texture);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}