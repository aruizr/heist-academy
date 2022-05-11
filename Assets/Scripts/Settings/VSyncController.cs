using Codetox.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class VSyncController : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private BoolVariable vSync;

        private void Awake()
        {
            QualitySettings.vSyncCount = vSync.Value ? 1 : 0;
            toggle.isOn = vSync.Value;
        }

        public void SetVSync(bool value)
        {
            vSync.Value = value;
            QualitySettings.vSyncCount = value ? 1 : 0;
        }
    }
}