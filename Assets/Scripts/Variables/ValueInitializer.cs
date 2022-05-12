using System;
using UnityEngine;
using UnityEngine.Events;

namespace Variables
{
    public abstract class ValueInitializer<T> : MonoBehaviour
    {
        [Flags]
        public enum InitializationStrategy
        {
            Awake = 1,
            Start = 2,
            OnEnable = 4
        }

        public ValueReference<T> valueReference;
        public InitializationStrategy initializationStrategy;
        public UnityEvent<T> onValueInitialized;

        private void Awake()
        {
            if (initializationStrategy.HasFlag(InitializationStrategy.Awake))
                onValueInitialized?.Invoke(valueReference.Value);
        }

        private void Start()
        {
            if (initializationStrategy.HasFlag(InitializationStrategy.Start))
                onValueInitialized?.Invoke(valueReference.Value);
        }

        private void OnEnable()
        {
            if (initializationStrategy.HasFlag(InitializationStrategy.OnEnable))
                onValueInitialized?.Invoke(valueReference.Value);
        }
    }
}