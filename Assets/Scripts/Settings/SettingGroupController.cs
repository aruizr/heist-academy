using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings
{
    public class SettingGroupController : MonoBehaviour
    {
        private List<SettingController> _settings;

        private void Awake()
        {
            _settings = GetComponents<SettingController>().ToList();
        }

        public void ResetGroup()
        {
            foreach (var setting in _settings) setting.ResetValue();
        }
    }
}