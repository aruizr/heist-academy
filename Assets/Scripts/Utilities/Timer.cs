using Codetox.Attributes;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Utilities
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private ValueReference<float> time;
        [SerializeField] [Disabled] private float currentValue;
        [SerializeField] private bool loop;
        [SerializeField] private bool autoPlay;
        [SerializeField] private UnityEvent onFinish;

        private bool _isPlaying;

        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            currentValue += Time.fixedDeltaTime;
            if (currentValue < time.Value) return;
            onFinish?.Invoke();
            if (loop) Rewind();
            else Stop();
        }

        private void OnEnable()
        {
            if (autoPlay) Play();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void Play()
        {
            _isPlaying = true;
        }

        public void Pause()
        {
            _isPlaying = false;
        }

        public void Stop()
        {
            _isPlaying = false;
            currentValue = 0;
        }

        public void Rewind()
        {
            currentValue = 0;
        }
    }
}