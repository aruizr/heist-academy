using UnityEngine;
using UnityEngine.Localization.Components;

namespace UI
{
    public class LocalizedUIDisplayController : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;

        public void Show(string key)
        {
            localizeStringEvent.SetEntry(key);
            localizeStringEvent.gameObject.SetActive(true);
        }
    }
}