using System;
using System.Collections.Generic;
using Codetox.Messaging;
using Codetox.Variables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Utilities;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Variable<InputControlScheme> currentScheme;
        [SerializeField] private string keyboardAndMouseControlSchemeName;
        [SerializeField] private List<UIPanel> panels;
        [SerializeField] private bool setFirstPanelOnEnable;

        private void OnEnable()
        {
            SetCursorState(currentScheme.Value);

            currentScheme.OnValueChanged += SetCursorState;

            if (panels.Count > 0 && setFirstPanelOnEnable) SetPanel(panels[0].panel);
        }

        private void OnDisable()
        {
            currentScheme.OnValueChanged -= SetCursorState;
        }

        private void SetCursorState(InputControlScheme scheme)
        {
            if (scheme.name == keyboardAndMouseControlSchemeName)
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

            panels.ForEach(uiPanel =>
            {
                var p = uiPanel.panel;
                p.SetActive(false);
            });
            EventSystem.current.SetSelectedGameObject(null);
            selected.panel.SetActive(true);

            if (!Cursor.visible && selected.firstSelected)
                EventSystem.current.SetSelectedGameObject(selected.firstSelected);
        }

        public void AddPanel(GameObject panel)
        {
            if (!panels.TryFind(uiPanel => uiPanel.panel.Equals(panel), out var selected)) return;
            EventSystem.current.SetSelectedGameObject(null);
            selected.panel.Send<CanvasGroup>(group =>
            {
                group.alpha = 1;
                group.interactable = true;
                group.blocksRaycasts = true;
            });
        }

        public void HideAll()
        {
            panels.ForEach(uiPanel =>
            {
                var p = uiPanel.panel;
                p.SetActive(true);
                p.Send<CanvasGroup>(group =>
                {
                    group.alpha = 0;
                    group.interactable = false;
                    group.blocksRaycasts = false;
                });
            });
        }

        [Serializable]
        public class UIPanel
        {
            public GameObject panel;
            public GameObject firstSelected;
        }
    }
}