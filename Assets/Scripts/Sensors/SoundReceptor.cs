using Codetox.Core;
using Codetox.GameEvents;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Sensors
{
    public class SoundReceptor : MonoBehaviour, ISoundReceptor
    {
        [SerializeField] private ValueReference<LayerMask> playerLayer;
        [SerializeField] private VoidGameEvent soundAlertEvent;
        
        public UnityEvent<Vector3> onSoundReceived;

        public void ReceiveSound(GameObject source)
        {
            if (source.IsInLayerMask(playerLayer.Value) && soundAlertEvent) soundAlertEvent.Invoke();
            onSoundReceived?.Invoke(source.transform.position);
        }
    }
}