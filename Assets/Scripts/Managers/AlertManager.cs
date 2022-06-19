using System.Linq;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class AlertManager : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet susEnemies;

        public UnityEvent onAlertStarted;
        public UnityEvent onAlertStopped;

        private bool _isSus;

        private void OnEnable()
        {
            susEnemies.OnItemAdded += StartAlert;
            susEnemies.OnItemRemoved += StopAlert;
        }

        private void StartAlert(GameObject obj)
        {
            if (_isSus) return;
            if (!susEnemies.Any()) return;
            _isSus = true;
            onAlertStarted?.Invoke();
        }

        private void StopAlert(GameObject obj)
        {
            if (!_isSus) return;
            if (susEnemies.Any()) return;
            _isSus = false;
            onAlertStopped?.Invoke();
        }
    }
}