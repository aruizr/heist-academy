using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace UI
{
    public class InfoTextController : MonoBehaviour
    {
        [SerializeField] private Transform container;

        private readonly List<DisplayData> _displayed = new List<DisplayData>();

        public void Show(GameObject obj)
        {
            var data = new DisplayData {OriginalParent = obj.transform.parent, GameObject = obj};

            _displayed.Add(data);
            obj.transform.SetParent(container, false);
            obj.SetActive(true);
        }

        public void Hide(GameObject obj)
        {
            var data = _displayed.Find(data => data.GameObject.Equals(obj));
            
            if (data == null) return;
            
            var o = data.GameObject;

            o.SetActive(false);
            o.transform.SetParent(data.OriginalParent, false);
            _displayed.Remove(data);
        }

        private class DisplayData
        {
            public Transform OriginalParent;
            public GameObject GameObject;
        }
    }
}