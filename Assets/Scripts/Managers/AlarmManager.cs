using System.Linq;
using RuntimeSets;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class AlarmManager : MonoBehaviour
    {
        [SerializeField] private GameObjectRuntimeSet alertedEnemies;

        public UnityEvent onAlarmStarted;
        public UnityEvent onAlarmStopped;

        private bool _isAlarmStarted;

        private void OnEnable()
        {
            alertedEnemies.OnItemAdded += StartAlarm;
            alertedEnemies.OnItemRemoved += StopAlarm;
        }
        
        private void StartAlarm(GameObject obj)
        {
            if (_isAlarmStarted) return;
            if (!alertedEnemies.Any()) return;
            _isAlarmStarted = true;
            onAlarmStarted?.Invoke();
        }

        private void StopAlarm(GameObject obj)
        {
            if (!_isAlarmStarted) return;
            if (alertedEnemies.Any()) return;
            _isAlarmStarted = false;
            onAlarmStopped?.Invoke();
        }
    }
}