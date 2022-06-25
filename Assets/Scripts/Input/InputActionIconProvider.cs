using System;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionIconProvider : MonoBehaviour
    {
        [SerializeField] private ControlSchemeSwitchedGameEvent gameEvent;
        [SerializeField] private InputActionReference inputActionReference;
        [SerializeField] private InputIconMap inputIconMap;
        [SerializeField] private TMP_SpriteAsset spriteAsset;

        private void OnEnable()
        {
            gameEvent.AddListener(OnControlSchemeSwitched);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(OnControlSchemeSwitched);
        }

        private void OnControlSchemeSwitched(ControlSchemeSwitchedGameEventData data)
        {
            try
            {
                var inputAction = inputActionReference.action;
                var currentBindingIndex = inputAction.GetBindingIndex(data.ControlScheme.bindingGroup);
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