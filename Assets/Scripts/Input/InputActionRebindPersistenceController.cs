using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputActionRebindPersistenceController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        public UnityEvent onLoadRebinds;
        public UnityEvent onResetRebinds;

        private void OnEnable()
        {
            LoadRebinds();
        }

        private void OnDisable()
        {
            SaveRebinds();
        }

        public void LoadRebinds()
        {
            var rebinds = PlayerPrefs.GetString(inputActionAsset.name + "-rebinds");
            if (!string.IsNullOrEmpty(rebinds)) inputActionAsset.LoadBindingOverridesFromJson(rebinds);
            onLoadRebinds?.Invoke();
        }

        public void SaveRebinds()
        {
            var rebinds = inputActionAsset.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(inputActionAsset.name + "-rebinds", rebinds);
        }

        public void ResetRebinds()
        {
            inputActionAsset.RemoveAllBindingOverrides();
            PlayerPrefs.DeleteKey(inputActionAsset.name + "-rebinds");
            onResetRebinds?.Invoke();
        }
    }
}