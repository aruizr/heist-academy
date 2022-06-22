using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private string keyboardAndMouseControlSchemeName;
        [SerializeField] private List<UIPanel> panels;

        private InputDevice _currentDevice;
        private InputControlScheme _currentScheme;

        private void OnEnable()
        {
            InputSystem.onEvent += OnInputSystemEvent;

            if (panels.Count > 0) SetPanel(panels[0].panel);
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

            if (_currentScheme.name == keyboardAndMouseControlSchemeName)
                ShowCursor();
            else
                HideCursor();
        }

        public void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            var e = EventSystem.current;
            
            e.SetSelectedGameObject(null);

            var data = new PointerEventData(e)
            {
                position = Mouse.current.position.ReadValue()
            };

            var results = new List<RaycastResult>();

            e.RaycastAll(data, results);

            foreach (var result in results)
            {
                if (!result.gameObject.TryGetComponent(out Selectable selectable)) continue;
                e.SetSelectedGameObject(result.gameObject);
                break;
            }

            if (e.currentSelectedGameObject == null)
                e.SetSelectedGameObject(panels.Find(panel => panel.panel.activeSelf).firstSelected);

            // EventSystem.current.SetSelectedGameObject(null);
            // EventSystem.current.SetSelectedGameObject(panels.Find(panel => panel.panel.activeSelf).firstSelected);
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void SetPanel(GameObject panel)
        {
            if (!panels.TryFind(uiPanel => uiPanel.panel.Equals(panel), out var selected)) return;

            panels.ForEach(uiPanel => uiPanel.panel.SetActive(false));
            EventSystem.current.SetSelectedGameObject(null);
            selected.panel.SetActive(true);

            if (!Cursor.visible) EventSystem.current.SetSelectedGameObject(selected.firstSelected);
        }

        [Serializable]
        public class UIPanel
        {
            public GameObject panel;
            public GameObject firstSelected;
        }
    }
}