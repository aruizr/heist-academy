using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VSyncController : MonoBehaviour
    {
        private const string Key = "v-sync";
        private const string Default = "default-v-sync";

        [SerializeField] private Toggle toggle;

        private void Start()
        {
            var value = QualitySettings.vSyncCount;
            
            PlayerPrefs.SetInt(Default, value);
            PlayerPrefs.Save();
            
            if (PlayerPrefs.HasKey(Key))
            {
                value = PlayerPrefs.GetInt(Key);
                QualitySettings.vSyncCount = value;
            }
            
            toggle.SetIsOnWithoutNotify(value.ToBool());
        }

        public void SetVSync(bool active)
        {
            var value = active.ToInt();

            QualitySettings.vSyncCount = value;
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public void ResetVSync()
        {
            toggle.isOn = PlayerPrefs.GetInt(Default).ToBool();
        }
    }
}