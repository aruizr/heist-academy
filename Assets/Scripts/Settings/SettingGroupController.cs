using UnityEngine;

namespace Settings
{
    public class SettingGroupController : MonoBehaviour
    {
        private SettingController[] _settings;

        private void Awake()
        {
            _settings = GetComponentsInChildren<SettingController>();
        }

        public void ResetGroup()
        {
            foreach (var setting in _settings) setting.ResetValue();
        }
    }
}