using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Audio
{
    public class StepAudioClipPicker : MonoBehaviour
    {
        [SerializeField] private List<StepSet> stepSets;
        
        public UnityEvent<AudioClip> onStepPicked;

        private StepSet _currentStepSet;
        private int _lastIndex;

        [Serializable]
        public class StepSet
        {
            public string surface;
            public List<AudioClip> clips;
        }

        public void SetCurrentSurface(GameObject surface)
        {
            var surfaceName = surface.tag;
            _currentStepSet = stepSets.FirstOrDefault(stepSet => stepSet.surface == surfaceName);
            _lastIndex = 0;
        }

        public void PickStepClip()
        {
            if (_currentStepSet == null) return;
            
            var index = 0;
            var clips = _currentStepSet.clips;

            do
            {
                index = Random.Range(0, clips.Count);
            } while (index == _lastIndex);

            var clip = clips[index];
            _lastIndex = index;

            onStepPicked?.Invoke(clip);
        }
    }
}