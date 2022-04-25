using UnityEngine;
using UnityEngine.Events;

namespace Interactions
{
    public class SoundReceptor : MonoBehaviour, ISoundReceptor
    {
        [SerializeField] private UnityEvent<Vector3> onSoundReceived;

        public void ReceiveSound(Vector3 sourcePoint)
        {
            onSoundReceived?.Invoke(sourcePoint);
        }
    }
}