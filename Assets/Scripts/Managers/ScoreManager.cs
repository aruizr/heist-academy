using System;
using System.Collections.Generic;
using Codetox.Variables;
using RuntimeSets;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private LevelEvaluation levelEvaluation;
        [SerializeField] private IntVariable alarmTriggerCount;
        [SerializeField] private IntVariable guardSoundAlertCount;
        [SerializeField] private IntVariable guardSightAlertCount;
        [SerializeField] private IntVariable cameraSightAlertCount;
        [SerializeField] private GameObjectRuntimeSet optionalObjectInventory;
        [SerializeField] private StringVariable levelTime;
        [SerializeField] private FloatVariable finalMark;

        private bool _isTimeCounting;
        private float _timeCounter;
        
        private void FixedUpdate()
        {
            if (!_isTimeCounting) return;
            _timeCounter += Time.fixedDeltaTime;
        }

        public void ResetStats()
        {
            alarmTriggerCount.Value = 0;
            guardSoundAlertCount.Value = 0;
            guardSightAlertCount.Value = 0;
            cameraSightAlertCount.Value = 0;
            _timeCounter = 0;
            optionalObjectInventory.Clear();
        }

        public void CalculateStats()
        {
            levelTime.Value = TimeSpan.FromSeconds(_timeCounter).ToString("m\\:ss");
            finalMark.Value = levelEvaluation.GetLevelMark();
        }

        public void OnAlarmTriggered()
        {
            alarmTriggerCount.Value++;
        }

        public void OnGuardSoundAlert()
        {
            guardSoundAlertCount.Value++;
        }

        public void OnGuardSightAlert()
        {
            guardSightAlertCount.Value++;
        }

        public void OnCameraSightAlert()
        {
            cameraSightAlertCount.Value++;
        }

        public void StartLevelTimeCounter()
        {
            _timeCounter = 0f;
            _isTimeCounting = true;
        }
        
        public void ResumeLevelTimeCounter()
        {
            _isTimeCounting = true;
        }

        public void StopLevelTimeCounter()
        {
            _isTimeCounting = false;
        }
    }
}