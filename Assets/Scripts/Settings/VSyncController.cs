using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Settings
{
    public class VSyncController : MonoBehaviour
    {
        private const string Key = "v-sync";

        [SerializeField] private Toggle toggle;

        private void Start()
        {
            if (!PlayerPrefs.HasKey(Key))
            {
                toggle.isOn = QualitySettings.vSyncCount.ToBool();
                return;
            }

            var value = PlayerPrefs.GetInt(Key);

            QualitySettings.vSyncCount = value;
            toggle.isOn = value.ToBool();
        }

        public void SetVSync(bool active)
        {
            var value = active.ToInt();

            QualitySettings.vSyncCount = value;
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
    }
}