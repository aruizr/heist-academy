using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Audio
{
    public class AudioClipRandomizer : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;

        public UnityEvent<AudioClip> onPickedRandom;

        private int _lastIndex;

        public void PickRandom()
        {
            var index = 0;

            do
            {
                index = Random.Range(0, clips.Count);
            } while (index == _lastIndex);

            var clip = clips[index];
            _lastIndex = index;

            onPickedRandom?.Invoke(clip);
        }
    }
}