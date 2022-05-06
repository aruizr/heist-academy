using Codetox.Core;
using Codetox.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class SoundReceptor : MonoBehaviour, ISoundReceptor
    {
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private VoidGameEvent soundAlertEvent;
        
        public UnityEvent<Vector3> onSoundReceived;

        public void ReceiveSound(GameObject source)
        {
            if (source.IsInLayerMask(playerLayer) && soundAlertEvent) soundAlertEvent.Invoke();
            onSoundReceived?.Invoke(source.transform.position);
        }
    }
}