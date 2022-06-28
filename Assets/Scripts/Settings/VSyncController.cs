using Codetox.Variables;
using UnityEngine;
using Utilities;

namespace Settings
{
    public class VSyncController : SettingController
    {
        private const string Key = "v-sync";
        private const string Default = "default-v-sync";

        [SerializeField] private Variable<bool> variable;

        private void Awake()
        {
            var value = QualitySettings.vSyncCount;

            PlayerPrefs.SetInt(Default, value);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetInt(Key);
                QualitySettings.vSyncCount = value;
            }

            variable.Value = value.ToBool();
        }

        private void OnEnable()
        {
            variable.OnValueChanged += SetVSync;
        }

        private void OnDisable()
        {
            variable.OnValueChanged -= SetVSync;
        }

        public void SetVSync(bool active)
        {
            var value = active.ToInt();

            QualitySettings.vSyncCount = value;
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public override void ResetValue()
        {
            variable.Value = PlayerPrefs.GetInt(Default).ToBool();
        }
    }
}