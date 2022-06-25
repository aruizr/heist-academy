using System;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ControlSchemeSwitchedGameEvent gameEvent;
        [SerializeField] private string keyboardAndMouseControlSchemeName;
        [SerializeField] private List<UIPanel> panels;

        private void OnEnable()
        {
            gameEvent.AddListener(OnControlSchemeSwitched);

            if (panels.Count > 0) SetPanel(panels[0].panel);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(OnControlSchemeSwitched);
        }

        private void OnControlSchemeSwitched(ControlSchemeSwitchedGameEventData data)
        {
            if (data.ControlScheme.name == keyboardAndMouseControlSchemeName)
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